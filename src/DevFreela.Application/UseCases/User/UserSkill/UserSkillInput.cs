

using DevFreela.Application.UseCases.User.Common;
using MediatR;

namespace DevFreela.Application.UseCases.User.UserSkill;
public class UserSkillInput : IRequest<UserModelOutput>
{


    public UserSkillInput(Guid idUser,
        List<DomainEntity.Skill> skills)
    {
        IdUser = idUser;
        Skills = skills;
    }

    public Guid IdUser { get; private set; }
    public List<DomainEntity.Skill> Skills { get; private set; }
}
