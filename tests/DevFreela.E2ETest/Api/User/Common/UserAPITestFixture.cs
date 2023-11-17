
using DevFreela.UnitTests.Base;

namespace DevFreela.UnitTests.Api.User.Common;

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

}
