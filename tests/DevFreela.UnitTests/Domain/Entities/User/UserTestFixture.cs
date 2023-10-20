
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
    => "12345678";

    public DateTime GetValidBirthDate()
    => Faker.Person.DateOfBirth;

    public DomainEntity.User CreateValidUser()
    => new DomainEntity.User(
            GetValidName(),
            GetValidName(),
            GetValidPassword(),
            GetValidBirthDate()
    );

}
