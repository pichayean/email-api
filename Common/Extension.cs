public static class Extensions
{
    public static void ForEach<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, Action<TKey, TValue> invoke)
    {
        foreach (var kvp in dictionary)
            invoke(kvp.Key, kvp.Value);
    }

    private static string CorsPolicyName = "_stupidCorsPolicy";

    public static IServiceCollection AddAppCors(this IServiceCollection services)
    {
        return services.AddCors(options => options.AddPolicy(name: CorsPolicyName, builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
    }

    public static IServiceCollection AddSettingsLoader(this IServiceCollection services)
    {
        services.AddTransient<Settings>(s =>
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
}