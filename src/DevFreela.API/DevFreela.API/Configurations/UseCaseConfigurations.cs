using DevFreela.Application.UseCases.Project.CreateProject;
using DevFreela.Application.Validators;
using DevFreela.Domain.Domain.Authorization;
using DevFreela.Domain.Domain.Repository;
using DevFreela.Infrastructure.Authorization;
using DevFreela.Infrastructure.Persistence.Repository;
using FluentValidation;

namespace DevFreela.API.Configurations;

public static class UseCaseConfigurations
{

    public static IServiceCollection AddUseCases(
        this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(CreateProject).Assembly));
        services.AddRepositories();
        services.AddFluentValidation();
        return services;
    }

    private static IServiceCollection AddRepositories(
        this IServiceCollection services)
    {
        services.AddTransient<IProjectRepository, ProjectRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<ISkillRepository, SkillRepository>();
        services.AddTransient<IProjectCommentRepository, ProjectCommentRepository>();
        services.AddTransient<IAuthorization, Authorization>();
        return services;
    }

    private static IServiceCollection AddFluentValidation(
        this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(CreateUserInputValidator).Assembly);
        return services;
    }


}
