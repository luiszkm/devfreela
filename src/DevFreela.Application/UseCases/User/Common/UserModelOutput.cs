


using DevFreela.Domain.Domain.Entities;

namespace DevFreela.Application.UseCases.User.Common;
public class UserModelOutput
{
    public UserModelOutput(Guid id,
        string name,
        string email,
        DateTime birthDate,
        List<DomainEntity.Models.UserSkills> skills)
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
    public List<DomainEntity.Models.UserSkills> Skills { get; private set; }


    public static UserModelOutput FromUser(DomainEntity.User user)
    {
        return new UserModelOutput(user.Id, user.Name, user.Email, user.BirthDate, user.Skills);
    }
}
