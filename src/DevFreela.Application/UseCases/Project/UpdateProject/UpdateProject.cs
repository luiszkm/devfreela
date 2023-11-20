
using DevFreela.Application.Exceptions;
using DevFreela.Application.UseCases.Project.Common;
using DevFreela.Domain.Domain.Repository;

namespace DevFreela.Application.UseCases.Project.UpdateProject;
public class UpdateProject : IUpdateProject
{
    private readonly IProjectRepository _projectRepository;

    public UpdateProject(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ProjectModelOutput> Handle(
        UpdateProjectInput request,
        CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetById(request.Id, cancellationToken);

        if (project == null)
            throw new NotFoundException();


        project.Update(request.Title, request.Description, request.TotalCost);

        return ProjectModelOutput.FromProject(project);
    }

}
