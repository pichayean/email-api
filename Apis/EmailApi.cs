namespace email_api.Apis;
public class EmailApi : IApi
{
    public EmailApi()
    {
    }

    public void Register(WebApplication app)
    {
        app.MapPost("/email/send", SendEmail).RequireAuthorization();
    }

    private IResult SendEmail(SendingEmail request, Settings settings)
    {
        // (new EmailSender(request, stmpSettings)).Send();
        return Results.Ok(new { success = true });
    }
}