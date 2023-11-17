using DevFreela.Domain.Domain.Enums;
using DevFreela.IntegrationTest.Base;

namespace DevFreela.IntegrationTest.Infra.Repositories.User;

[CollectionDefinition(nameof(UserRepositoryTestFixture))]

public class UserRepositoryTestFixtureCollection : ICollectionFixture<UserRepositoryTestFixture>
{
}
public class UserRepositoryTestFixture : BaseFixture
{
    public DomainEntity.User GetValidUser(UserRole role = UserRole.Client)
        => new(
            GetValidName(),
            GetValidEmail(),
            GetValidPassword(),
            GetValidBirthDate(),
            role);

    public List<DomainEntity.User> GetExampleUsersList(int length = 10)
        => Enumerable.Range(1, length)
            .Select(_ => GetValidUser())
            .ToList();

}
