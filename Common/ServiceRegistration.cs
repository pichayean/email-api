using email_api.Database;
using Microsoft.EntityFrameworkCore;

public static class ServiceRegistration
{
    public static IServiceCollection AddDatabase(this IServiceCollection services
        , IConfiguration configuration)
    {
        string connectionString = configuration.GetValue<string>("ConnectionString:EmailDB");
        services.AddDbContext<EmailContext>(options => options.UseNpgsql(connectionString));
        // services.AddStackExchangeRedisCache(options =>
        // {
        //     options.Configuration = redisConnectionString;
        // });
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
}