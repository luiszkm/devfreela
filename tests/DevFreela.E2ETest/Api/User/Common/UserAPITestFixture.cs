
using DevFreela.E2ETest.Base;

namespace DevFreela.E2ETest.Api.User.Common;

[CollectionDefinition(nameof(UserAPITestFixture))]
public class UserE2EFixtureCollection : ICollectionFixture<UserAPITestFixture>
{
}
public class UserAPITestFixture : BaseFixture
{
    public UserPersistence Persistence;

    public UserAPITestFixture() : base()
    {
        Persistence = new UserPersistence(CreateApiDbContextInMemory());
    }

    public Guid GetValidId()
        => Guid.NewGuid();

    public bool GetRandomBool()
        => new Random().Next(0, 2) == 0;

    public UserUseCases.CreateUser.CreateUserInput GetUserInput()
        => new(
            GetValidName(),
            GetValidEmail(),
            GetValidBirthDate(),
            GetValidPassword(),
            0);


    public string GetInvalidPassword()
        => Faker.Internet.Password(5);

    public UserUseCases.UpdateUser.UpdateUserInput
        GetUpdateUserInput(Guid? userId = null, bool? userActive = null)
        => new(
            userId ?? GetValidId(),
            GetValidName(),
            GetValidEmail(),
            GetValidBirthDate(),
            userActive ?? GetRandomBool()
            );


}
