var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddAppCors()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddDatabase(builder.Configuration)
    .AddRedis(builder.Configuration)
    .AddTransient<IApi, EmailApi>()
    .AddTransient<IApi, SecurityApi>()
    .AddTransient<IApi, HealthApi>()
    .AddScoped<ISettingService, SettingLoader>()
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
app.UseApis();

// app.Urls.Add($"http://localhost:{builder.Configuration["Port"]}");
app.Run();
