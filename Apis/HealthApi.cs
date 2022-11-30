using System.Security.Cryptography;
namespace email_api.Apis;
public class HealthApi : IApi
{

    public HealthApi()
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