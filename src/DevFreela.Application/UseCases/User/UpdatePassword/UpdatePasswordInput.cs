

using MediatR;

namespace DevFreela.Application.UseCases.User.UpdatePassword;
public class UpdatePasswordInput : IRequest
{
    public UpdatePasswordInput(
        Guid id,
        string oldPassword,
        string newPassword)
    {
        Id = id;
        OldPassword = oldPassword;
        NewPassword = newPassword;
    }

    public Guid Id { get; private set; }
    public string OldPassword { get; private set; }
    public string NewPassword { get; private set; }
}
