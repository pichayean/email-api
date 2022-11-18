var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddAppCors()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddDatabase(builder.Configuration)
    .AddTransient<IApi, EmailApi>()
    .AddTransient<IApi, SecurityApi>()
    .AddTransient<IApi, HealthApi>()
    .AddTransient<ISettingService, SettingLoader>()
    .AddJwtAuthentication()
    .AddAuthorization()
    .AddSettingsLoader();

var app = builder.Build();
app.UseAppCors()
    .UseGlobalExceptionHandler()
    .UseSwagger()
    .UseSwaggerUI()
    .UseAuthentication()
    .UseAuthorization();
var apis = app.Services.GetServices<IApi>();
foreach (var api in apis)
    api.Register(app);

// app.Urls.Add($"http://localhost:{builder.Configuration["Port"]}");
app.Run();
