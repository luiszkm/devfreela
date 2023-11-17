
using MediatR;

namespace DevFreela.Application.UseCases.Session;
public interface ISession : IRequestHandler<SessionInput, SessionOutput>
{
}
