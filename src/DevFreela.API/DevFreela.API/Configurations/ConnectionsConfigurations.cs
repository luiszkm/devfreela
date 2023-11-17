
using DevFreela.Domain.Domain.Interfaces;
using DevFreela.Infrastructure.MessageBus;
using DevFreela.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.API.Configurations;

public static class ConnectionsConfigurations
{
    public static IServiceCollection AddAppConnections(
        this IServiceCollection services,
        IConfiguration config)
    {
        // services.AddDbConnection(config);
        //services.AddDbConnectionDev(config);
        services.AddInMemoryConnections();
        services.AddGatewayOfPayment();
        services.AddRabbitMq();
        return services;
    }

    private static IServiceCollection AddDbConnection(
        this IServiceCollection services,
        IConfiguration config)
    {

        var connectionString = config.GetConnectionString("DevFreela");
        var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
        connectionStringBuilder.Encrypt = false;
        connectionStringBuilder.TrustServerCertificate = true;

        services.AddDbContext<DevFreelaDbContext>(
            options => options.UseSqlServer(connectionStringBuilder.ToString()));

        return services;
    }

    private static IServiceCollection AddDbConnectionDev(
        this IServiceCollection services,
        IConfiguration config)
    {
        var connectionString = config.GetConnectionString("DevFreelaDb");
        services.AddDbContext<DevFreelaDbContext>(
            options => options.UseMySql(connectionString,
                ServerVersion.AutoDetect(connectionString)));

        return services;
    }
    public static IServiceCollection AddInMemoryConnections(
        this IServiceCollection services)
    {
        services.AddDbContext<DevFreelaDbContext>(
            options => options.UseInMemoryDatabase("DevFreela-In-Memory"));

        return services;
    }

    public static IServiceCollection AddGatewayOfPayment(
        this IServiceCollection services)
    {
        services.AddHttpClient();

        return services;
    }

    public static IServiceCollection AddRabbitMq(
        this IServiceCollection services)
    {
        services.AddScoped<IMessageBusService, MessageBusService>();

        return services;
    }

}
