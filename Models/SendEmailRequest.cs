
using System.ComponentModel.DataAnnotations;

namespace email_api.Models;

public class SendEmailRequest
{
    [Required]
    public string[] EmailsTo { get; set; }
    [Required]
    public bool UsedTemplate { get; set; }
    [Required]
    public string BodyPlainText { get; set; }
    [Required]
    public string Subject { get; set; }
    public Dictionary<string, string>? EmbeddedImages { get; set; }
}