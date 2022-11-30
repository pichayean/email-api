namespace email_api.Database;
public partial class RefreshTokenEntity:BaseEntity
{
    public string Email { get; set; }
    public string RefreshToken { get; set; }
    public string ReferenceCode { get; set; }
    public DateTime Expired { get; set; }
}