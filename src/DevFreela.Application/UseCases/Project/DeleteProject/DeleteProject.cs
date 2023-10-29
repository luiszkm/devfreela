

using DevFreela.Domain.Domain.Repository;

namespace DevFreela.Application.UseCases.Project.DeleteProject;
internal class DeletePRoject : IDeleteProject
{
    private readonly IProjectRepository _projectRepository;

    public DeletePRoject(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task Handle(DeleteProjectInput request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetById(request.Id, cancellationToken);
        if (project == null) return;

        await _projectRepository.Delete(project, cancellationToken);
    }
}
