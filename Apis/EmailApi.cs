namespace email_api.Apis;
public class EmailApi : IApi
{
    private readonly StmpSettings stmpSettings;
    public EmailApi(IConfiguration configuration)
    {
        this.stmpSettings = configuration.GetSection("Stmp").Get<StmpSettings>();
    }

    public void Register(WebApplication app)
    {
        app.MapPost("/email/send", SendEmail).RequireAuthorization();
    }

    private IResult SendEmail(SendingEmail request)
    {
        // (new EmailSender(request, stmpSettings)).Send();
        return Results.Ok(new { success = true });
    }
}