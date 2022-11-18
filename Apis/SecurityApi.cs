using email_api.Database;

namespace email_api.Apis;
public class SecurityApi : IApi
{
    private readonly JwtSettings jwtSettings;

    public SecurityApi(IConfiguration configuration)
    {
        this.jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>();
    }

    public void Register(WebApplication app)
    {
        app.MapPost("/security/OTPVerification", OTPVerification);
        app.MapPost("/security/RequestOTP", RequestOTP);
        app.MapPost("/security/RefreshToken", RefreshToken).RequireAuthorization();
    }

    private IResult OTPVerification(EmailContext emailContext, OtpVerificationModel request)
    {
        var backlisted = emailContext.BlackList.Any(_ => _.Email.Equals(request.email));
        if (backlisted)
            return Results.Unauthorized();

        var otp = emailContext.Otp.FirstOrDefault(_ => _.Email.Equals(request.email)
            && _.ReferenceCode.Equals(request.referenceCode));
        if (otp is null)
            return Results.Unauthorized();

        if (otp.InvalidCount >= 4)
            return Results.Ok(new
            {
                StatusCode = 401,
                Message = "Invalid code limit",
            });
        if (otp.InvalidCount == 99)
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

        var authState = new AuthenticationHistory
        {
            Id = Guid.NewGuid(),
            Email = request.email,
            RefreshToken = Security.GenerateRefreshToken(),
            ReferenceCode = otp.ReferenceCode,
            Expired = DateTime.Now.AddDays(10)
        };
        emailContext.AuthenticationHistory.Add(authState);
        emailContext.SaveChanges();

        return Results.Ok(new
        {
            RefreshTokenExpired = authState.Expired,
            authState.RefreshToken,
            AccessToken = Security.GenerateToken(request.email, authState.ReferenceCode, jwtSettings),
        });
    }

    private IResult RequestOTP(EmailContext emailContext, RequestOtpModel request)
    {
        var backlisted = emailContext.BlackList.Any(_ => _.Email.Equals(request.email));
        if (backlisted)
            return Results.Unauthorized();

        var otp = new Otp
        {
            Id = Guid.NewGuid(),
            Code = Common.RandomCode(5),
            Email = request.email,
            ReferenceCode = Common.RandomRefCode(15),
            Expired = DateTime.Now.AddMinutes(5)
        };
        emailContext.Otp.Add(otp);
        emailContext.SaveChanges();

        return Results.Ok(new
        {
            otp.ReferenceCode,
            otp.Expired
        });
    }

    private IResult RefreshToken([FromHeader(Name = "Authorization")] string header, RefreshTokenModel request, EmailContext emailContext)
    {
        var jwt = header.ToString().Split(" ").Last();
        var principal = Security.GetPrincipalFromExpiredToken(jwt, jwtSettings);
        var referenceCode = principal.Claims.FirstOrDefault(x => x.Type == MacusClaimsIdentity.ReferenceCode);
        var email = principal.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email);
        var refresh = emailContext.AuthenticationHistory
            .FirstOrDefault(_ => _.RefreshToken.Equals(request.refreshToken.ToString())
                && _.ReferenceCode.Equals(referenceCode.Value)
                && _.Email.Equals(email.Value));
        if (DateTime.Now > refresh.Expired)
            throw new SecurityTokenException("Invalid token");

        return Results.Ok(new
        {
            AccessToken = Security.GenerateToken(email.Value, referenceCode.Value, jwtSettings),
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