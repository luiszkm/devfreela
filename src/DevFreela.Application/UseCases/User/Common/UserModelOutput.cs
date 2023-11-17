


using DevFreela.Domain.Domain.Entities;

namespace DevFreela.Application.UseCases.User.Common;
public class UserModelOutput
{
    public UserModelOutput(Guid id,
        string name,
        string email,
        DateTime birthDate,
        List<DomainEntity.Skill> skills)
    {
        Id = id;
        Name = name;
        Email = email;
        BirthDate = birthDate;
        Skills = skills;

    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public DateTime BirthDate { get; private set; }
    public List<DomainEntity.Skill> Skills { get; private set; }

    public string Password { get; private set; }

    public static UserModelOutput FromUser(DomainEntity.User user)
    {
        return new UserModelOutput(user.Id, user.Name, user.Email, user.BirthDate, new List<DomainEntity.Skill>());
    }
}
