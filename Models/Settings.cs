public class Settings : ISettings
{
    public int RefreshTokenLifetime { get; set; }

    public string JwtIssuer { get; set; }
    public string JwtAudience { get; set; }
    public int JwtLifetime { get; set; }
    public string JwtSecret { get; set; }

    public string StmpSecrectKey { get; set; }
    public string StmpHost { get; set; }
    public int StmpPort { get; set; }
    public string StmpUser { get; set; }

    public int OtpLength { get; set; }
    public int OtpRefCodeLength { get; set; }
    public int OtpLifetime { get; set; }
    public int OtpInvalidAllowTime { get; set; }
    public int OtpSuccessCode { get; set; }
}