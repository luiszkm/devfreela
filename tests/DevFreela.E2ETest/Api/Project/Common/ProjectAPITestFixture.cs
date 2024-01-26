

using DevFreela.Application.UseCases.Project.UpdateProject;
using DevFreela.E2ETest.Base;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

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



    public async Task<List<DomainEntity.User>>
        GetListUser(int? listUserAmount = 5)
    {
        var lsitUsers = new List<DomainEntity.User>();
        var password = GetValidPassword();
        for (int i = 0; i < listUserAmount; i++)
        {
            lsitUsers.Add(GetValidUser(password: password));
        }

        return lsitUsers;
    }

    public async Task<DomainEntity.Project> CreateProjectInDataBase(
        Guid userId,
        bool listFreelancerInterested = false,
        DomainEntity.User? user = null)
    {
        var dbContext = CreateApiDbContextInMemory();
        var project = GetValidProject(userId);

        if (listFreelancerInterested)
        {
            var listFreelancer = await GetListUser(5);
            foreach (var freelancer in listFreelancer)
            {
                if (!dbContext.FreelancersInterested.Any(
                        fi => fi.IdProject == project.Id &&
                              fi.IdFreelancer == freelancer.Id))
                {
                    project.AddFreelancersInterested(freelancer);
                    await dbContext.FreelancersInterested.AddAsync(
                        new Models.FreelancersInterested(project.Id, freelancer.Id));
                }
            }
        }

        if (user != null)
        {
            if (!dbContext.FreelancersInterested.Any(
                    fi => fi.IdProject == project.Id &&
                          fi.IdFreelancer == user.Id))
            {
                project.AddFreelancersInterested(user);
                await dbContext.FreelancersInterested.AddAsync(
                    new Models.FreelancersInterested(project.Id, user.Id));
            }

        }

        await dbContext.Projects.AddAsync(project);
        await dbContext.SaveChangesAsync();

        return project;
    }

}
