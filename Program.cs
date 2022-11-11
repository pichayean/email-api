const string CorsPolicyName = "_stupidCorsPolicy";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options
  => options.AddPolicy(name: CorsPolicyName, builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));


var stmpSettings = builder.Configuration.GetSection("Stmp").Get<StmpSettings>();
var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = Security.TokenValidationParameters(jwtSettings);
});
builder.Services.AddAuthorization();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(CorsPolicyName);
app.UseExceptionHandler(exceptionHandlerApp =>
 {
     exceptionHandlerApp.Run(async context =>
     {
         var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

         string message = exceptionHandlerPathFeature?.Error.Message;
         var stackTrace = exceptionHandlerPathFeature?.Error.StackTrace;

         var exceptionResult = JsonSerializer.Serialize(new { error = message, stackTrace });
         context.Response.ContentType = "application/json";
         context.Response.StatusCode = StatusCodes.Status500InternalServerError;

         await context.Response.WriteAsync(exceptionResult);
     });
 });
app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/security/createToken", [AllowAnonymous] (User user) =>
{
    var token = Security.SignIn(user, jwtSettings);
    if (!string.IsNullOrEmpty(token))
        return Results.Ok(new { token = token });
    return Results.Unauthorized();
});

app.MapPost("/email/send", (SendingEmail request) =>
{
    (new EmailSender(request, stmpSettings)).Send();
    return Results.Ok(new { success = true });
}).RequireAuthorization();

app.UseAuthentication();

app.UseAuthorization();

// app.Urls.Add($"http://localhost:{builder.Configuration["Port"]}");
app.Run();