using System.Security.Cryptography;

namespace email_api.Features;

public class Security
{
    public static string GenerateToken(string email, string referenceCode, Settings jwtSettings)
    {
        var key = Encoding.ASCII.GetBytes(jwtSettings.JwtSecret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(MacusClaimsIdentity.ReferenceCode, referenceCode),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(jwtSettings.JwtLifetime),
            Issuer = jwtSettings.JwtIssuer,
            Audience = jwtSettings.JwtAudience,
            SigningCredentials = new SigningCredentials
            (new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);
        var stringToken = tokenHandler.WriteToken(token);
        // Console.Write(stringToken);
        return stringToken;
    }
    public static TokenValidationParameters TokenValidationParameters(Settings jwtSettings)
    {
        return new TokenValidationParameters
        {
            ValidIssuer = jwtSettings.JwtIssuer,
            ValidAudience = jwtSettings.JwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(jwtSettings.JwtSecret)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };
    }

    public static string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public static ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token, Settings jwtSettings)
    {
        var tokenValidationParameters = TokenValidationParameters(jwtSettings);

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;

    }
}