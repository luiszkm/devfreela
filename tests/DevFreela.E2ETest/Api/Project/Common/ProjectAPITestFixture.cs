

using DevFreela.Application.UseCases.Project.UpdateProject;
using DevFreela.E2ETest.Base;
using DevFreela.Infrastructure.Persistence;

namespace DevFreela.E2ETest.Api.Project.Common;

[CollectionDefinition(nameof(ProjectAPITestFixture))]

public class CreateProjectTestFixtureCollection : ICollectionFixture<ProjectAPITestFixture> { }
public class ProjectAPITestFixture : BaseFixture
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



    public UpdateProjectInput GetUpdateProjectInputModel(Guid ProjectId)
        => new(
            ProjectId,
            GetValidName(),
            GetValidDescription(),
            GetValidTotalCost());


    public async Task<DomainEntity.Project> CreateProjectInDataBase(Guid userId)
    {
        var dbContext = CreateApiDbContextInMemory();
        var project = GetValidProject(userId);
        await dbContext.Projects.AddAsync(project);
        await dbContext.SaveChangesAsync();

        return project;
    }

}
