
using DevFreela.Application.UseCases.User.Common;
using MediatR;

namespace DevFreela.Application.UseCases.User.UpdateUser;
public interface IUpdateUser : IRequestHandler<UpdateUserInput, UserModelOutput>
{
}
