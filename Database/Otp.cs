namespace email_api.Database;
public partial class Otp
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string ReferenceCode { get; set; }
    public string Code { get; set; }
    public DateTime Expired { get; set; }
    public int InvalidCount { get; set; }
}