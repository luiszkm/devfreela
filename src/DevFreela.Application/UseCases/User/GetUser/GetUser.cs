

using DevFreela.Application.Exceptions;
using DevFreela.Application.UseCases.User.Common;
using DevFreela.Domain.Domain.Repository;

namespace DevFreela.Application.UseCases.User.GetUser;
public class GetUser : IGetUser
{

    private readonly IUserRepository _userRepository;
    public GetUser(IUserRepository userRepository)
    => _userRepository = userRepository;

    public async Task<UserModelOutput>
        Handle(GetUserInput request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.Id, cancellationToken);

        if (user == null)
            throw new NotFoundException();


        return UserModelOutput.FromUser(user);
    }
}
