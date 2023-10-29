using DevFreela.Application.Commands.CreateProject;

namespace DevFreela.API.Configurations;

public static class UseCaseConfigurations
{

    public static IServiceCollection AddUseCases(
        this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(CreateProjectCommand).Assembly));

        return services;
    }
}
