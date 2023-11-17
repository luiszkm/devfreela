

using DevFreela.Application.Exceptions;
using DevFreela.Domain.Domain.Repository;

namespace DevFreela.Application.UseCases.User.UpdateUser;
public class Updateuser : IUpdateUser
{
    private readonly IUserRepository _userRepository;

    public Updateuser(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(UpdateUserInput request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.Id, cancellationToken);
        if (user == null)
            throw new NotFoundException("User not found");
        user.Update(request.Name, request.Email, request.BirthDate);
    }
}
