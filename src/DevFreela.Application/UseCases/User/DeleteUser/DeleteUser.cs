
using DevFreela.Application.Exceptions;
using DevFreela.Domain.Domain.Repository;

namespace DevFreela.Application.UseCases.User.DeleteUser;
internal class DeleteUser : IDeleteUser
{
    private readonly IUserRepository _userRepository;

    public DeleteUser(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(DeleteUserInput request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.Id, cancellationToken);
        if (user == null)
            throw new NotFoundException();

        await _userRepository.Delete(user, cancellationToken);
    }
}
