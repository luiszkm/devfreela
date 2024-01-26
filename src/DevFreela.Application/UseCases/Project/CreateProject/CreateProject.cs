using DevFreela.Application.UseCases.Project.Common;
using DevFreela.Domain.Domain.Repository;

namespace DevFreela.Application.UseCases.Project.CreateProject;
public class CreateProject : ICreateProject
{
    private readonly IProjectRepository _projectRepository;

    public CreateProject(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ProjectModelOutput> Handle(
        CreateProjectInput request,
        CancellationToken cancellationToken)
    {
        var project = new DomainEntity.Project(
            request.Title,
            request.Description,
            request.TotalCost,
            request.IdClient);


        await _projectRepository.Create(project, cancellationToken);

        return ProjectModelOutput.FromProject(project);
    }
}


