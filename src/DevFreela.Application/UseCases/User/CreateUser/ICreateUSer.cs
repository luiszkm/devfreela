

using DevFreela.Application.UseCases.User.Common;
using MediatR;

namespace DevFreela.Application.UseCases.User.CreateUser;

public interface ICreateUSer : IRequestHandler<CreateUserInput, UserModelOutput> { }
