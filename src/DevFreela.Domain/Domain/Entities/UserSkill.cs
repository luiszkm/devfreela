

using DevFreela.Domain.Domain.seddwork;

namespace DevFreela.Domain.Domain.Entities;

public class UserSkill : AgregateRoot
{
    public UserSkill(int idUser, int idSkill)
    {
        IdUser = idUser;
        IdSkill = idSkill;
    }

    public int IdUser { get; private set; }
    public int IdSkill { get; private set; }

    public Skill Skill { get; private set; }

}