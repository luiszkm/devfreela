

using DevFreela.Domain.Domain.seddwork;

namespace DevFreela.Domain.Domain.Entities;

public class UserSkill : AggregateRoot
{
    public UserSkill(Guid idUser, Guid idSkill)
    {
        IdUser = idUser;
        IdSkill = idSkill;
    }

    public Guid IdUser { get; private set; }
    public Guid IdSkill { get; private set; }

    public Skill Skill { get; private set; }

}