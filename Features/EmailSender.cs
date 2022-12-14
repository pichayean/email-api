using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using MimeKit.Utils;

namespace email_api.Features;

public class EmailSender
{
    private readonly StmpSettings _settings;
    private readonly SendingEmail _sendingEmail;

    public EmailSender(SendingEmail sendingEmail
        , StmpSettings settings)
    {
        _sendingEmail = sendingEmail;
        _settings = settings;
    }

    public void Send()
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse($"{_sendingEmail.EmailFrom}"));
        foreach (var e in _sendingEmail.EmailsTo)
            email.To.Add(MailboxAddress.Parse(e));
        email.Subject = _sendingEmail.Subject;
        if (_sendingEmail.UsedTemplate)
            email.Body = BuildBody(_sendingEmail.BodyPlainText, _sendingEmail.EmbeddedImages).ToMessageBody();
        else
            email.Body = new TextPart(TextFormat.Html) { Text = _sendingEmail.BodyPlainText };

        using (var smtp = new SmtpClient())
        {
            smtp.Connect(_settings.Stmp, _settings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate($"{_settings.EmailUser}", _settings.SecrectKey);
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
