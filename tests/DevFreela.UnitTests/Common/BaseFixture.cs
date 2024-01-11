
using Bogus;
using DevFreela.Domain.Domain.Enums;

namespace DevFreela.UnitTests.Common;
public class BaseFixture
{
    public Faker Faker { get; set; }

    public BaseFixture()
        => Faker = new Faker("pt_BR");
    public Decimal GetRandomDecimal()
        => new Random().Next(1, 10000);

    public Guid GetValidGuid()
        => Guid.NewGuid();

    public string GetValidDescription()
        => Faker.Lorem.Paragraph();


    public DomainEntity.Project CreateValidProject(Guid? ClientId = null)
        => new DomainEntity.Project(
            GetValidName(),
            GetValidDescription(),
            GetRandomDecimal(),
            ClientId ?? GetValidGuid());

    public List<DomainEntity.User> GetListUsers(int total = 10)
    {
        var list = new List<DomainEntity.User>();

        for (int i = 0; i < total; i++)
        {
            list.Add(GetValidUser());
        }

        return list;
    }
    public string GetValidName()
        => Faker.Person.FullName;

    public string GetValidEmail()
        => Faker.Person.Email;

    public string GetValidPassword()
        => Faker.Internet.Password();


    public DateTime GetValidBirthDate()
        => Faker.Person.DateOfBirth;

    public DomainEntity.User GetValidUser()
        => new(
            GetValidName(),
            GetValidName(),
            GetValidPassword(),
            GetValidBirthDate(),
            UserRole.Client);

    public DomainEntity.Skill GetValidSkill()
        => new(GetValidName());

    public List<DomainEntity.Skill> GetValidSkillList()
    {
        var skills = new List<DomainEntity.Skill>();
        for (int i = 0; i < 5; i++)
        {
            skills.Add(new DomainEntity.Skill(
                Faker.Company.Random.Words()));
        }

        return skills;
    }
    public DomainEntity.ProjectComment GetValidProjectComment(
        Guid? projectId = null,
        Guid? userId = null)
    => new(
        Faker.Lorem.Paragraph(),
        projectId ?? Guid.NewGuid(),
        userId ?? Guid.NewGuid());

    public DomainEntity.Models.ProjectSkills GetValidProjectSkill(
        Guid? projectId = null,
        Guid? skillId = null)
        => new(
            projectId ?? Guid.NewGuid(),
            skillId ?? Guid.NewGuid());




}
