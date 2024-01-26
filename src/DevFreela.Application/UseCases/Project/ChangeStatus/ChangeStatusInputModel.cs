
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

    public enum ProjectStatusEnum
    {
        Created = 0,
        InProgress = 1,
        Suspended = 2,
        Cancelled = 3,
        Finished = 4,
        PaymentPending = 5
    }
}
