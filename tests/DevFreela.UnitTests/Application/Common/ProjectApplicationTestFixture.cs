

using DevFreela.Domain.Domain.Repository;
using DevFreela.UnitTests.Common;
using Moq;

namespace DevFreela.UnitTests.Application.Common;


[CollectionDefinition(nameof(ProjectApplicationTestFixture))]

public class ProjectApplicationTestFixtureCollection : ICollectionFixture<ProjectApplicationTestFixture> { }

public class ProjectApplicationTestFixture : BaseFixture
{
    public string GetValidTitle()
        => Faker.Commerce.ProductName();

    public string GetValidDescription()
    => Faker.Lorem.Paragraph();

    public decimal GetValidTotalCost()
        => Faker.Random.Decimal(100, 1000);

    public ProjectUseCase.CreateProject.CreateProjectInput GetValidInputModel()
        => new(
            GetValidTitle(),
            GetValidDescription(),
            GetValidTotalCost(),
            Guid.NewGuid());


    public Mock<IProjectRepository> GetProjectRepositoryMock()
        => new();

    public Mock<IUserRepository> GetUserRepositoryMock()
        => new();
    public Mock<IProjectRepository> GetUserRepositoryMockWithProject()
    {
        var mock = GetProjectRepositoryMock();

        return mock;
    }
}
