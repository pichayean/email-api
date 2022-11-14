namespace email_api.Apis;
public class HealthApi : IApi
{

    public HealthApi(IConfiguration configuration)
    {
    }

    public void Register(WebApplication app)
    {
        app.MapGet("/health/ready", () =>
        {
            return Results.Ok(new { success = true });
        });
    }
}