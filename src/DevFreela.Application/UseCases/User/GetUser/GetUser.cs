

using DevFreela.Application.Exceptions;
using DevFreela.Application.UseCases.User.Common;
using DevFreela.Domain.Domain.Repository;

namespace DevFreela.Application.UseCases.User.GetUser;
public class GetUser : IGetUser
{

    private readonly IUserRepository _userRepository;
    private readonly ISkillRepository _skillRepository;
    public GetUser(IUserRepository userRepository,
        ISkillRepository skillRepository)
    {
        _userRepository = userRepository;
        _skillRepository = skillRepository;
    }

    public async Task<UserModelOutput>
        Handle(GetUserInput request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.Id, cancellationToken);

        if (user == null)
            throw new NotFoundException();


        var userSkills = await _userRepository.GetUserWithSkills(request.Id);
        var skills = new List<DomainEntity.Skill>();

        if (userSkills != null)
        {
            foreach (var skill in userSkills)
            {
                var skillModel = _skillRepository.GetById(skill.IdSkill);
                skills.Add(skillModel);
            }
            user.AddSkills(skills);
        }

        ;

        return UserModelOutput.FromUser(user, skills);
    }
}
