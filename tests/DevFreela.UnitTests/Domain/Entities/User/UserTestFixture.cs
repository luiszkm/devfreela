
using DevFreela.Domain.Domain.Enums;
using DevFreela.UnitTests.Common;

namespace DevFreela.UnitTests.Domain.Entities.User;

[CollectionDefinition(nameof(UserTestFixture))]
public class UserTestFixtureCollection : ICollectionFixture<UserTestFixture>
{
}
public class UserTestFixture : BaseFixture
{
    public string GetValidName()
    => Faker.Person.FullName;

    public string GetValidEmail()
    => Faker.Person.Email;

    public string GetValidPassword()
    => Faker.Internet.Password();


    public DateTime GetValidBirthDate()
    => Faker.Person.DateOfBirth;

    public DomainEntity.User CreateValidUser()
    => new(
            GetValidName(),
            GetValidName(),
            GetValidPassword(),
            GetValidBirthDate(),
            UserRole.Client
    );




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




}
