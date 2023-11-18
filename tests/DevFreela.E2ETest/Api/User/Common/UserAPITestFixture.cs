
using DevFreela.E2ETest.Base;

namespace DevFreela.E2ETest.Api.User.Common;

[CollectionDefinition(nameof(UserAPITestFixture))]
public class UserE2EFixtureCollection : ICollectionFixture<UserAPITestFixture>
{
}
public class UserAPITestFixture : BaseFixture
{


    public UserUseCases.CreateUser.CreateUserInput GetUserInput()
        => new(
            GetValidName(),
            GetValidEmail(),
            GetValidBirthDate(),
            GetValidPassword(),
            0);

    public string GetInvalidName()
        => "a";
    public string GetInvalidPassword()
        => "1234567";
    public string GetInvalidEmail()
        => "aco";


}
