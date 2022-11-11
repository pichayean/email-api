namespace email_api.Models;

public class StmpSettings
{
    public string SecrectKey { get; set; }
    public string Stmp { get; set; }
    public int Port { get; set; }
    public string EmailUser { get; set; }
}