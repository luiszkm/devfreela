

using DevFreela.Application.Exceptions;
using DevFreela.Domain.Domain.Enums;
using DevFreela.Domain.Domain.Repository;

namespace DevFreela.Application.UseCases.Project.ChangeStatus;
public class ChangeStatus : IChangeStatus
{
    private readonly IProjectRepository _projectRepository;

    public ChangeStatus(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task Handle(ChangeStatusInputModel request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetById(request.Id, cancellationToken);
        if (project == null)
            throw new NotFoundException();

        project.ChangeStatus((ProjectStatusEnum)request.Status);

    }
}
