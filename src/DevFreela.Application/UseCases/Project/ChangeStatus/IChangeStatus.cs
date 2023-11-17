
using MediatR;

namespace DevFreela.Application.UseCases.Project.ChangeStatus;
public interface IChangeStatus : IRequestHandler<ChangeStatusInputModel>
{
}
