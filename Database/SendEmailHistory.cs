// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable
namespace email_api.Database;
public partial class SendEmailHistory
{
    public Guid Id { get; set; }
    public string Token { get; set; }
    public string Request { get; set; }
    public DateTime CreatedDate { get; set; }
}