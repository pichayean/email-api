using System.ComponentModel.DataAnnotations;

namespace email_api.Models;

public record OtpVerificationModel
{
    [Required]
    public string email { get; init; }
    [Required]
    public string code { get; init; }
    [Required]
    public string referenceCode { get; init; }
}
public record RequestOtpModel
{
    [Required]
    public string email { get; init; }
}
public record RefreshTokenModel
{
    [Required]
    public string refreshToken { get; init; }
}
