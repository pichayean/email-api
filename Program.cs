var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddAppCors()
    .AddEndpointsApiExplorer()
    .AddJwtAuthentication(builder.Configuration)
    .AddAuthorization()
    .AddSwaggerGen()
    .AddDatabase(builder.Configuration)
    .AddTransient<IApi, EmailApi>()
    .AddTransient<IApi, SecurityApi>()
    .AddTransient<IApi, HealthApi>();

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


// services.AddSingleton<CustomerIdentitySettings>(serviceProvider =>
//   {
//       return serviceProvider.GetRequiredService<ISettingService>().LoadSettingAsync<CustomerIdentitySettings>().Result;
//   });

// app.Urls.Add($"http://localhost:{builder.Configuration["Port"]}");
app.Run();
