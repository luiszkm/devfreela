

using DevFreela.Application.Exceptions;
using DevFreela.Domain.Domain.Enums;
using DevFreela.Infrastructure.Models;
using DevFreela.Infrastructure.Persistence.Repository;

namespace DevFreela.IntegrationTest.Infra.Repositories.Project;

[Collection(nameof(ProjectRepositoryTestFixture))]
public class ProjectRepositoryTest : IDisposable
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
    [Trait("Infra", "ProjectRepository - Repository")]

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

    [Fact(DisplayName = nameof(AddFreelancerInterested))]
    [Trait("Infra", "ProjectRepository - Repository")]

    public async Task AddFreelancerInterested()
    {
        var dbContext = _fixture.CreateDbContext();
        var project = _fixture.GetValidProject();
        var user = _fixture.GetValidUser();

        await dbContext.Projects.AddAsync(project);
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();

        var projectRepository = new ProjectRepository(dbContext);

        await projectRepository.AddFreelancerInterested(
            project.Id,
            user.Id,
            CancellationToken.None);

        var projectUpdated = await projectRepository.GetById(project.Id, CancellationToken.None);

        projectUpdated.FreelancersInterested.Should().NotBeNullOrEmpty();

        dbContext.Projects.Should().Contain(project);
        dbContext.Users.Should().Contain(user);
        dbContext.FreelancersInterested.Should().NotBeNullOrEmpty();
        dbContext.FreelancersInterested.Should().Contain(
            f => f.IdProject == project.Id && f.IdFreelancer == user.Id);

    }


    [Fact(DisplayName = nameof(RemoveFreelancerInterested))]
    [Trait("Infra", "ProjectRepository - Repository")]

    public async Task RemoveFreelancerInterested()
    {
        var dbContext = _fixture.CreateDbContext();
        var project = _fixture.GetValidProject();
        var user = _fixture.GetValidUser();
        var freelancerInterested = _fixture.GetValidFreelancersInterested(project.Id, user.Id);

        await dbContext.Projects.AddAsync(project);
        await dbContext.Users.AddAsync(user);
        await dbContext.FreelancersInterested.AddAsync(freelancerInterested);
        await dbContext.SaveChangesAsync();

        var projectRepository = new ProjectRepository(dbContext);

        await projectRepository.RemoveFreelancerInterested(
            project.Id,
            user.Id,
            CancellationToken.None);

        var projectUpdated = await projectRepository.GetById(project.Id, CancellationToken.None);

        projectUpdated.FreelancersInterested.Should().BeNullOrEmpty();

        dbContext.Projects.Should().Contain(project);
        dbContext.Users.Should().Contain(user);
        dbContext.FreelancersInterested.Should().BeNullOrEmpty();
        dbContext.FreelancersInterested.Should().NotContain(
       f => f.IdProject == project.Id && f.IdFreelancer == user.Id);

    }
    [Fact(DisplayName = nameof(RemoveFreelancerInterestedWithMany))]
    [Trait("Infra", "ProjectRepository - Repository")]

    public async Task RemoveFreelancerInterestedWithMany()
    {
        var dbContext = _fixture.CreateDbContext();
        var project = _fixture.GetValidProject(withFreelancersInterested: true);
        var listFreelancerInterested = new List<FreelancersInterested>();

        foreach (var freelancer in project.FreelancersInterested)
        {
            listFreelancerInterested.Add(new FreelancersInterested(
                project.Id,
                freelancer.Id));
        }

        await dbContext.Projects.AddAsync(project);
        await dbContext.FreelancersInterested.AddRangeAsync(listFreelancerInterested);
        await dbContext.SaveChangesAsync();
        var freelancerToRemove = listFreelancerInterested[0];
        var projectRepository = new ProjectRepository(dbContext);

        await projectRepository.RemoveFreelancerInterested(
            project.Id,
            freelancerToRemove.IdFreelancer,
            CancellationToken.None);

        var projectUpdated = await projectRepository.GetById(project.Id, CancellationToken.None);

        projectUpdated.FreelancersInterested.Should().NotBeNullOrEmpty();

        dbContext.Projects.Should().Contain(project);
        dbContext.FreelancersInterested.Should().NotBeNullOrEmpty();
        dbContext.FreelancersInterested.Should().NotContain(
            f => f.IdProject == project.Id && f.IdFreelancer == project.FreelancersInterested[0].Id);
        projectUpdated.FreelancersInterested.Should().HaveCount(project.FreelancersInterested.Count - 1);

    }

    [Fact(DisplayName = nameof(ContractFreelancer))]
    [Trait("Infra", "ProjectRepository - Repository")]
    public async Task ContractFreelancer()
    {
        var freelancerToContract = _fixture.GetValidUser();
        var dbContext = _fixture.CreateDbContext();
        var project = _fixture.GetValidProject(withFreelancersInterested: false);

        dbContext.Projects.Add(project);
        dbContext.Users.Add(freelancerToContract);
        await dbContext.SaveChangesAsync();

        var projectRepository = new ProjectRepository(dbContext);

        await projectRepository.ContractFreelancer(
            project.Id,
            freelancerToContract.Id,
            CancellationToken.None);



        var projectUpdated = await projectRepository.GetById(project.Id, CancellationToken.None);
        projectUpdated.IdFreelancer.Should().Be(freelancerToContract.Id);
        projectUpdated.Status.Should().Be(ProjectStatusEnum.InProgress);

        freelancerToContract.FreelanceProjects.Should().NotBeNullOrEmpty();
        freelancerToContract.FreelanceProjects.Should().Contain(
                       f => f.Id == project.Id && f.IdFreelancer == freelancerToContract.Id);

        dbContext.Projects.Should().Contain(project);
        dbContext.FreelancerOwnedProjects.Should().NotBeNullOrEmpty();
        dbContext.FreelancerOwnedProjects.Should().Contain(
                       f => f.IdProject == project.Id && f.IdUser == freelancerToContract.Id);


    }

    [Fact(DisplayName = nameof(ThrowWhenNotTryingFoundProject))]
    [Trait("Infra", "ProjectRepository - Repository")]
    public async Task ThrowWhenNotTryingFoundProject()
    {
        var freelancerToContract = _fixture.GetValidUser();
        var dbContext = _fixture.CreateDbContext();
        var project = _fixture.GetValidProject(withFreelancersInterested: false);

        dbContext.Users.Add(freelancerToContract);
        await dbContext.SaveChangesAsync();

        var projectRepository = new ProjectRepository(dbContext);

        var action = async () => await projectRepository.ContractFreelancer(
              project.Id,
              freelancerToContract.Id,
              CancellationToken.None);


        action.Should().ThrowAsync<NotFoundException>();

        dbContext.Projects.Should().NotContain(project);
    }


    public void Dispose()
    {
        _fixture.ClearDatabase();
    }
}
