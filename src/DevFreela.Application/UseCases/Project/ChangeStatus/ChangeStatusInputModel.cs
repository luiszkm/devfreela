
using DevFreela.Domain.Domain.Enums;
using MediatR;

namespace DevFreela.Application.UseCases.Project.ChangeStatus;

public class ChangeStatusInputModel : IRequest
{
    public ChangeStatusInputModel(
        Guid id,
        ProjectStatusEnum status)
    {
        Id = id;
        Status = status;
    }

    public Guid Id { get; private set; }
    public ProjectStatusEnum Status { get; private set; }


}
