using email_api.Database;
using Microsoft.EntityFrameworkCore;

public static class ServiceRegistration
{
    public static IServiceCollection AddDatabase(this IServiceCollection services
        , IConfiguration configuration)
    {
        string connectionString = configuration.GetValue<string>("ConnectionString:EmailDB");
        services.AddDbContext<EmailContext>(options => options.UseNpgsql(connectionString));
        return services;
    }

    public static IServiceCollection AddRedis(this IServiceCollection services
        , IConfiguration configuration)
    {
        string redisConnectionString = configuration.GetValue<string>("ConnectionString:Redis");
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisConnectionString;
        });
        return services;
    }

    private static string CorsPolicyName = "_stupidCorsPolicy";

    public static IServiceCollection AddAppCors(this IServiceCollection services)
    {
        return services.AddCors(options => options.AddPolicy(name: CorsPolicyName, builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
    }

    public static IServiceCollection AddSettingsLoader(this IServiceCollection services)
    {
        services.AddScoped<Settings>(s =>
        {
            return s.GetRequiredService<ISettingService>().LoadSettingAsync<Settings>().Result;
        });
        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
    {
        var sp = services.BuildServiceProvider();
        var settings = sp.GetRequiredService<ISettingService>().LoadSettingAsync<Settings>().Result;
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.TokenValidationParameters = Security.TokenValidationParameters(settings);
        });
        return services;
    }

    public static IApplicationBuilder UseAppCors(this IApplicationBuilder app)
    {
        return app.UseCors(CorsPolicyName);
    }

    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        return app.UseExceptionHandler(exceptionHandlerApp =>
        {
            exceptionHandlerApp.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                string? message = exceptionHandlerPathFeature?.Error.Message;
                var stackTrace = exceptionHandlerPathFeature?.Error.StackTrace;

                var exceptionResult = JsonSerializer.Serialize(new { error = message, stackTrace });
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                await context.Response.WriteAsync(exceptionResult);
            });
        });
    }

    public static IApplicationBuilder UseApis(this WebApplication app)
    {
        var apis = app.Services.GetServices<IApi>();
        foreach (var api in apis)
            api.Register(app);
        return app;
    }
}