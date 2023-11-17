

using DevFreela.Application.Exceptions;
using DevFreela.Domain.Domain.Enums;
using DevFreela.Infrastructure.Persistence.Repository;

namespace DevFreela.IntegrationTest.Infra.Repositories.Project;

[Collection(nameof(ProjectRepositoryTestFixture))]
public class ProjectRepositoryTest
{
    private readonly ProjectRepositoryTestFixture _fixture;

    public ProjectRepositoryTest(ProjectRepositoryTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(InsertProject))]
    [Trait("Infra", "UserRepository - Repository")]

    public async Task InsertProject()
    {
        var dbContext = _fixture.CreateDbContext();
        var project = _fixture.GetValidProject();

        var projectRepository = new ProjectRepository(dbContext);
        projectRepository.Create(project, CancellationToken.None);

        dbContext.Projects.Should().Contain(project);
    }

    [Fact(DisplayName = nameof(GetProject))]
    [Trait("Infra", "UserRepository - Repository")]

    public async Task GetProject()
    {
        var dbContext = _fixture.CreateDbContext();
        var project = _fixture.GetValidProject();
        var projectList = _fixture.GetExampleProjectList();

        await dbContext.Projects.AddRangeAsync(projectList);
        await dbContext.Projects.AddAsync(project);
        await dbContext.SaveChangesAsync();

        var projectRepository = new ProjectRepository(dbContext);

        var projectFound = await projectRepository.GetById(project.Id, CancellationToken.None);

        projectFound.Should().BeEquivalentTo(project);
        projectFound.Id.Should().Be(project.Id);
        projectFound.Title.Should().Be(project.Title);
        projectFound.Description.Should().Be(project.Description);
        projectFound.TotalCost.Should().Be(project.TotalCost);
        projectFound.IdClient.Should().Be(project.IdClient);
    }

    [Fact(DisplayName = nameof(ThrowWhenNotFoundProject))]
    [Trait("Infra", "UserRepository - Repository")]

    public async Task ThrowWhenNotFoundProject()
    {
        var dbContext = _fixture.CreateDbContext();
        var project = _fixture.GetValidProject();
        var projectList = _fixture.GetExampleProjectList();

        await dbContext.Projects.AddRangeAsync(projectList);
        await dbContext.SaveChangesAsync();

        var projectRepository = new ProjectRepository(dbContext);

        var action = async () =>
            await projectRepository.GetById(project.Id, CancellationToken.None);

        action.Should().ThrowAsync<NotFoundException>();

    }

    [Fact(DisplayName = nameof(UpdateProject))]
    [Trait("Infra", "UserRepository - Repository")]
    public async Task UpdateProject()
    {
        var dbContext = _fixture.CreateDbContext();
        var project = _fixture.GetValidProject();
        var projectList = _fixture.GetExampleProjectList();

        await dbContext.Projects.AddRangeAsync(projectList);
        await dbContext.SaveChangesAsync();

        var projectRepository = new ProjectRepository(dbContext);

        projectList[0].Update(
            project.Title,
            project.Description,
            project.TotalCost);

        await projectRepository.Update(projectList[0], CancellationToken.None);
        var projectUpdated = await projectRepository.GetById(projectList[0].Id, CancellationToken.None);

        projectUpdated.Should().BeEquivalentTo(projectList[0]);
        projectUpdated.Id.Should().Be(projectList[0].Id);
        projectUpdated.Title.Should().Be(project.Title);
        projectUpdated.Description.Should().Be(project.Description);
        projectUpdated.TotalCost.Should().Be(project.TotalCost);

    }

    [Fact(DisplayName = nameof(DeleteProject))]
    [Trait("Infra", "UserRepository - Repository")]
    public async Task DeleteProject()
    {
        var dbContext = _fixture.CreateDbContext();
        var project = _fixture.GetValidProject();
        var projectList = _fixture.GetExampleProjectList();

        await dbContext.Projects.AddRangeAsync(projectList);
        await dbContext.Projects.AddAsync(project);
        await dbContext.SaveChangesAsync();

        var projectRepository = new ProjectRepository(dbContext);

        await projectRepository.Delete(project, CancellationToken.None);

        dbContext.Projects.Should().NotContain(project);

    }


    [Fact(DisplayName = nameof(ChangeStatusProject))]
    [Trait("Infra", "UserRepository - Repository")]

    public async Task ChangeStatusProject()
    {
        var dbContext = _fixture.CreateDbContext();
        var project = _fixture.GetValidProject();

        await dbContext.Projects.AddAsync(project);
        await dbContext.SaveChangesAsync();

        var projectRepository = new ProjectRepository(dbContext);

        await projectRepository.ChangeStatus(project.Id, ProjectStatusEnum.InProgress, CancellationToken.None);

        var projectUpdated = await projectRepository.GetById(project.Id, CancellationToken.None);

        projectUpdated.Status.Should().Be(ProjectStatusEnum.InProgress);

    }


}
