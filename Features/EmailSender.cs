using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using MimeKit.Utils;

namespace email_api.Features;

public class EmailSender
{
    const string _secrectKey = "";
    const string _emailFrom = "pichayeanyensiri.work@gmail.com";
    private readonly string[] _emailTo;
    private readonly bool _usedTemplate;
    private string _bodyPlainText = "";
    private Dictionary<string, string>? _embeddedImages;
    private string _subject = "";

    public EmailSender(bool usedTemplate
        , string[] emailTo
        , string subject
        , string plainText
        , Dictionary<string, string>? embeddedImages = null)
    {
        _usedTemplate = usedTemplate;
        _bodyPlainText = plainText;
        _subject = subject;
        _emailTo = emailTo;
        _embeddedImages = embeddedImages;
    }

    public void Send()
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse($"{_emailFrom}"));
        foreach (var e in _emailTo)
            email.To.Add(MailboxAddress.Parse(e));
        email.Subject = _subject;
        if (_usedTemplate)
        {
            var builder = BuildBody(_bodyPlainText, _embeddedImages);
            email.Body = builder.ToMessageBody();
        }
        else
        {
            email.Body = new TextPart(TextFormat.Html) { Text = _bodyPlainText };
        }
        
        using (var smtp = new SmtpClient())
        {
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate($"{_emailFrom}", _secrectKey);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }

    public BodyBuilder BuildBody(string plainText, Dictionary<string, string>? embeddedImages)
    {
        var builder = new BodyBuilder();
        if (embeddedImages is not null)
        {
            embeddedImages.ForEach((key, base64) => 
            {
                var image = builder.LinkedResources.Add(key, Convert.FromBase64String(base64));
                image.ContentId = MimeUtils.GenerateMessageId();
                plainText.Replace($"{key}", image.ContentId);
                Console.WriteLine($"(Key: {key}, value: {base64})");
            });
        }
        builder.HtmlBody = plainText;
        return builder;
    }
}
