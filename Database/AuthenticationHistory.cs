namespace email_api.Database;
public partial class AuthenticationHistory
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string RefreshToken { get; set; }
    public string ReferenceCode { get; set; }
    public DateTime Expired { get; set; }
}