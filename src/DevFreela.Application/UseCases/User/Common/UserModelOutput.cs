



namespace DevFreela.Application.UseCases.User.Common;




public class UserModelOutput
{
    public UserModelOutput(Guid id,
        string name,
        string email,
        DateTime birthDate,
        List<DomainEntity.Skill> skills,
        IReadOnlyList<UserOwnedProjectModelOutput> ownedProjects)
    {
        Id = id;
        Name = name;
        Email = email;
        BirthDate = birthDate;
        Skills = skills;
        OwnedProjects = ownedProjects;
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public DateTime BirthDate { get; private set; }
    public List<DomainEntity.Skill> Skills { get; private set; }

    public IReadOnlyList<UserOwnedProjectModelOutput> OwnedProjects { get; set; }


    public class UserOwnedProjectModelOutput
    {
        public UserOwnedProjectModelOutput(Guid id,
            string? title = null)
        {
            Id = id;
            Title = title;
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; }



    }


    public static UserModelOutput FromUser(
        DomainEntity.User user,
        List<DomainEntity.Skill>? skills = null)
    {
        return new UserModelOutput(user.Id,
            user.Name,
            user.Email,
            user.BirthDate,
            skills,
            user.OwnedProjects.Select(
                p => new UserOwnedProjectModelOutput(p.Id, p.Title)).ToList()
            );
    }


}

