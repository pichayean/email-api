using email_api.Features;
using email_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace email_api.Controllers;

[ApiController]
[Route("[controller]")]
public class EmailController : ControllerBase
{
    [HttpPost(Name = "SendEmail")]
    public IActionResult SendEmail([FromBody] SendEmailRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var emailSender = new EmailSender(request.UsedTemplate
            , request.EmailsTo
            , request.Subject
            , request.BodyPlainText
            , request.EmbeddedImages);
        emailSender.Send();
        return Ok(new { success = true });
    }
}
