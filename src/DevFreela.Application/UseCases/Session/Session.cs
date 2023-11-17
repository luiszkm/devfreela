

using DevFreela.Application.Exceptions;
using DevFreela.Domain.Domain.Authorization;
using DevFreela.Domain.Domain.Repository;

namespace DevFreela.Application.UseCases.Session;
public class Session : ISession
{
    private readonly IAuthorization _authorization;
    private readonly IUserRepository _userRepository;

    public Session(
        IUserRepository userRepository,
        IAuthorization authorization)
    {
        _authorization = authorization;
        _userRepository = userRepository;
    }

    public async Task<SessionOutput> Handle(
        SessionInput request,
        CancellationToken cancellationToken)
    {
        var passwordHash = _authorization.ComputeSha256Hash(request.Password);

        var user = await _userRepository.GetUserByEmail(request.Email);


        if (user == null) throw new CredentialInvalid();

        //    if (user.Password != passwordHash) throw new CredentialInvalid();

        var token = _authorization.GenerateToken(user.Id, user.Role);

        var sessionOutput = new SessionOutput(
            user.Id,
            token);


        return sessionOutput;

    }
}
