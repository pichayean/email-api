namespace email_api.Apis;
public class SecurityApi : IApi
{
    private readonly JwtSettings jwtSettings;

    public SecurityApi(IConfiguration configuration)
    {
        this.jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>();
    }

    public void Register(WebApplication app)
    {
        app.MapPost("/security/createToken", Login);
    }

    private IResult Login(User user)
    {

        var token = Security.SignIn(user, jwtSettings);
        if (!string.IsNullOrEmpty(token))
            return Results.Ok(new { token = token });
        return Results.Unauthorized();
    }
}