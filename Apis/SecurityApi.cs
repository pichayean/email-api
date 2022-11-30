using System;
using email_api.Database;

namespace email_api.Apis;
public class SecurityApi : IApi
{

    public SecurityApi()
    {
    }

    public void Register(WebApplication app)
    {
        app.MapPost("/security/OTPVerification", OTPVerification);
        app.MapPost("/security/RequestOTP", RequestOTP);
        app.MapPost("/security/RefreshToken", RefreshToken).RequireAuthorization();
    }

    private IResult OTPVerification(EmailContext emailContext, OtpVerificationModel request, Settings settings)
    {
        var backlisted = emailContext.BlackList.Any(_ => _.Email.Equals(request.email));
        if (backlisted)
            return Results.Unauthorized();

        var otp = emailContext.Otp.FirstOrDefault(_ => _.Email.Equals(request.email)
            && _.ReferenceCode.Equals(request.referenceCode));
        if (otp is null)
            return Results.Unauthorized();

        if (otp.InvalidCount >= settings.OtpInvalidAllowTime)
            return Results.Ok(new
            {
                StatusCode = 401,
                Message = "Invalid code limit",
            });
        if (otp.InvalidCount == settings.OtpSuccessCode)
            return Results.Ok(new
            {
                StatusCode = 200,
                Message = "OTP Already used",
            });

        if (!otp.Code.Equals(request.code))
        {
            otp.InvalidCount++;
            emailContext.SaveChanges();
            return Results.Unauthorized();
        }

        var authState = new RefreshTokenEntity
        {
            Id = Guid.NewGuid(),
            Email = request.email,
            RefreshToken = Security.GenerateRefreshToken(),
            ReferenceCode = otp.ReferenceCode,
            Expired = DateTime.Now.AddDays(settings.RefreshTokenLifetime)
        };
        emailContext.RefreshToken.Add(authState);
        emailContext.SaveChanges();

        return Results.Ok(new
        {
            RefreshTokenExpired = authState.Expired,
            authState.RefreshToken,
            AccessToken = Security.GenerateToken(request.email, authState.ReferenceCode, settings),
        });
    }

    private IResult RequestOTP(EmailContext emailContext, RequestOtpModel request, Settings setting)
    {
        var backlisted = emailContext.BlackList.Any(_ => _.Email.Equals(request.email));
        if (backlisted)
            return Results.Unauthorized();

        var otp = new OtpEntity
        {
            Id = Guid.NewGuid(),
            Code = Common.RandomCode(setting.OtpLength),
            Email = request.email,
            ReferenceCode = Common.RandomRefCode(setting.OtpRefCodeLength),
            Expired = DateTime.Now.AddMinutes(setting.OtpLifetime)
        };
        emailContext.Otp.Add(otp);
        emailContext.SaveChanges();

        return Results.Ok(new
        {
            otp.ReferenceCode,
            otp.Expired
        });
    }

    private IResult RefreshToken([FromHeader(Name = "Authorization")] string header, RefreshTokenModel request, EmailContext emailContext, Settings setting)
    {
        var jwt = header.ToString().Split(" ").Last();
        var principal = Security.GetPrincipalFromExpiredToken(jwt, setting);
        var referenceCode = principal.Claims.FirstOrDefault(x => x.Type == MacusClaimsIdentity.ReferenceCode);
        var email = principal.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email);
        var refresh = emailContext.RefreshToken
            .FirstOrDefault(_ => _.RefreshToken.Equals(request.refreshToken.ToString())
                && _.ReferenceCode.Equals(referenceCode.Value)
                && _.Email.Equals(email.Value));
        if (DateTime.Now > refresh.Expired)
            throw new SecurityTokenException("Invalid token");

        return Results.Ok(new
        {
            AccessToken = Security.GenerateToken(email.Value, referenceCode.Value, setting),
        });
    }

    private IResult Revoke(string email)
    {
        return Results.Ok();
    }

    private IResult RevokeAll(string email)
    {
        return Results.Ok();
    }
}