
using System.ComponentModel.DataAnnotations;

namespace email_api.Models;

public class SendingEmail
{
    public string EmailFrom { get; set; }
    public string[] EmailsTo { get; set; }
    public bool UsedTemplate { get; set; }
    public string BodyPlainText { get; set; }
    public string Subject { get; set; }
    public Dictionary<string, string>? EmbeddedImages { get; set; }
}