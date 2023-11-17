
using DevFreela.Application.UseCases.User.Common;
using MediatR;

namespace DevFreela.Application.UseCases.User.GetUser;
public interface IGetUser : IRequestHandler<GetUserInput, UserModelOutput>
{
}
