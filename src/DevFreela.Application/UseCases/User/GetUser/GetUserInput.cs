
using DevFreela.Application.UseCases.User.Common;
using MediatR;

namespace DevFreela.Application.UseCases.User.GetUser;
public class GetUserInput : IRequest<UserModelOutput>
{
    public GetUserInput(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; private set; }
}
