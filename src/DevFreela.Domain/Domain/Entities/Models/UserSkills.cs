using DevFreela.Domain.Domain.seddwork;

namespace DevFreela.Domain.Domain.Entities.Models;
public class UserSkills : AggregateRoot
{
    public UserSkills(Guid idUser, Guid idSkill)
    {
        IdUser = idUser;
        IdSkill = idSkill;
    }
    public Guid IdUser { get; private set; }
    public Guid IdSkill { get; private set; }
    public User User { get; private set; }
    public Skill Skill { get; private set; }
}
