
using DevFreela.Application.Exceptions;
using DevFreela.Application.UseCases.User.Common;
using DevFreela.Domain.Domain.Authorization;
using DevFreela.Domain.Domain.Exceptions;
using DevFreela.Domain.Domain.Repository;

namespace DevFreela.Application.UseCases.User.CreateUser;
public class CreateUser : ICreateUSer
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthorization _authorization;
    public CreateUser(
        IUserRepository userRepository,
        IAuthorization authorization)
    {
        _userRepository = userRepository;
        _authorization = authorization;
    }
    public async Task<UserModelOutput>
        Handle(CreateUserInput request, CancellationToken cancellationToken)
    {
        var userAlreadyExists = await _userRepository.GetUserByEmail(request.Email);
        if (userAlreadyExists != null)
            throw new AggregateExistsExceptions("E-mail ja cadastrado");

        PasswordValidate(request.Password);
        var passwordHash = _authorization.ComputeSha256Hash(request.Password);


        var user = new DomainEntity.User(
                       request.Name,
                       request.Email,
                       passwordHash,
                       request.BirthDate,
                       request.Role);

        _userRepository.Create(user, cancellationToken);
        return UserModelOutput.FromUser(user);
    }
    private void PasswordValidate(string password)
    {
        if (string.IsNullOrEmpty(password))
            throw new EntityValidationExceptions("the password not match the security policies");
        if (password.Length < 8)
            throw new EntityValidationExceptions("the password not match the security policies");

    }
}
