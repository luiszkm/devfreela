

using DevFreela.Application.Exceptions;
using DevFreela.Application.UseCases.User.Common;
using DevFreela.Domain.Domain.Repository;

namespace DevFreela.Application.UseCases.User.UpdateUser;

public class Updateuser : IUpdateUser
{
    private readonly IUserRepository _userRepository;

    public Updateuser(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    public async Task<UserModelOutput> Handle(
        UpdateUserInput request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.Id, cancellationToken);
        if (user == null)
            throw new NotFoundException();
        user.Update(request.Name, request.Email, request.BirthDate);

        await _userRepository.Update(user, cancellationToken);
        return UserModelOutput.FromUser(user);
    }
}
