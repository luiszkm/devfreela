

using DevFreela.E2ETest.Base;
using DevFreela.Infrastructure.Persistence;

namespace DevFreela.E2ETest.Api.Project.Common;

[CollectionDefinition(nameof(CreateProjectTestFixture))]

public class CreateProjectTestFixtureCollection : ICollectionFixture<CreateProjectTestFixture> { }
public class CreateProjectTestFixture : BaseFixture
{
    public string GetValidDescription()
        => Faker.Lorem.Paragraph();

    public decimal GetValidTotalCost()
        => Faker.Random.Decimal(100, 1000);

    public Guid GetValidIdClient()
        => Guid.NewGuid();

    public ProjectUseCases.CreateProject.CreateProjectInput
        GetProjectInputModel(Guid? idClient = null)
        => new(
            GetValidName(),
            GetValidDescription(),
            GetValidTotalCost(),
            idClient ?? GetValidIdClient());

    public DomainEntity.Project GetValidProject(Guid? idClient = null)
        => new(
            GetValidName(),
            GetValidDescription(),
            GetValidTotalCost(),
            idClient ?? GetValidIdClient());



    public async Task<(DomainEntity.User user, string password)> GetUserInDataBase()
    {
        var password = GetValidPassword();
        var dbContext = CreateApiDbContextInMemory();
        var user = GetValidUser(password: password);
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();

        return (user, password);
    }
}
