

using DevFreela.Application.Exceptions;
using DevFreela.Domain.Domain.Authorization;
using DevFreela.Domain.Domain.Exceptions;
using DevFreela.Domain.Domain.Repository;
using MediatR;

namespace DevFreela.Application.UseCases.User.UpdatePassword;
public class UpdatePassword : IRequestHandler<UpdatePasswordInput>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthorization _authorization;


    public UpdatePassword(IUserRepository userRepository, IAuthorization authorization)
    {
        _userRepository = userRepository;
        _authorization = authorization;
    }

    public async Task Handle(
        UpdatePasswordInput request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.Id, cancellationToken);
        if (user == null)
            throw new NotFoundException();
        var oldPasswordHash = _authorization.ComputeSha256Hash(request.OldPassword);
        var newPasswordHash = _authorization.ComputeSha256Hash(request.NewPassword);




        user.UpdatePassword(oldPasswordHash, newPasswordHash);

        await _userRepository.UpdatePassword(user.Id, request.OldPassword, request.NewPassword);



    }
}
