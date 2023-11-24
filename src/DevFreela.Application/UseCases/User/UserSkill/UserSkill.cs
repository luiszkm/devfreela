

using DevFreela.Application.Exceptions;
using DevFreela.Application.UseCases.User.Common;
using DevFreela.Domain.Domain.Repository;
using MediatR;

namespace DevFreela.Application.UseCases.User.UserSkill;
public class UserSkill : IRequestHandler<UserSkillInput, UserModelOutput>
{
    private readonly IUserRepository _userRepository;

    public UserSkill(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserModelOutput>
        Handle(UserSkillInput request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.IdUser, cancellationToken);
        if (user == null)
            throw new NotFoundException();
        user.AddSkills(request.Skills);
        return UserModelOutput.FromUser(user);
    }
}
