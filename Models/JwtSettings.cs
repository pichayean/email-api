namespace email_api.Models;

public class JwtSettings
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int Expires { get; set; }
    public string Key { get; set; }
}