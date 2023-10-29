
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.API.Configurations;

public static class ConnectionsConfigurations
{
    public static IServiceCollection AddAppConnections(
        this IServiceCollection services,
        IConfiguration config)
    {
        //services.AddDbConnection(config);
        services.AddInMemoryConnections();

        return services;
    }

    private static IServiceCollection AddDbConnection(
        this IServiceCollection services,
        IConfiguration config)
    {
        var connectionString = config.GetConnectionString("DevFreela");
        services.AddDbContext<DevFreelaDbContext>(
            options => options.UseSqlServer(connectionString));

        return services;
    }
    public static IServiceCollection AddInMemoryConnections(
        this IServiceCollection services)
    {
        services.AddDbContext<DevFreelaDbContext>(
            options => options.UseInMemoryDatabase("DevFreela-dev"));

        return services;
    }
}
