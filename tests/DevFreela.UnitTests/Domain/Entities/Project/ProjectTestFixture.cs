
using Bogus;
using DevFreela.Domain.Domain.Entities;
using DevFreela.UnitTests.Common;

namespace DevFreela.UnitTests.Domain.Entities.Project;

[CollectionDefinition(nameof(ProjectTestFixture))]


public class ProjectTestFixtureCollection : ICollectionFixture<ProjectTestFixture>
{
}
public class ProjectTestFixture : BaseFixture
{
    public string GetValidTitle()
        => Faker.Commerce.ProductName();

    public string GetValidDescription()
        => Faker.Commerce.ProductDescription();

    public string GetValidPassword()
        => Faker.Internet.Password();

    public DateTime GetValidBirthDate()
        => Faker.Person.DateOfBirth;

    public Guid GetValidIdClient()
        => Guid.NewGuid();

    public DomainEntity.Project CreateValidProject()
        => new DomainEntity.Project(
            GetValidTitle(),
            GetValidDescription(),
            GetRandomDecimal(),
            GetValidIdClient());

    public decimal GetInvalidTotalCost()
        => Math.Round(Faker.Random.Decimal(min: -100, max: 0), 2);


    public DomainEntity.Project GetValidProjectWithFreelancerInterested(int total = 10)
    {
        var project = CreateValidProject();

        for (int i = 0; i < total; i++)
        {
            project.AddFreelancersInterested(GetValidUser());
        }

        return project;
    }

    public DomainEntity.Project GetValidProjectWithComments(int total = 10)
    {
        var project = CreateValidProject();

        for (int i = 0; i < total; i++)
        {
            project.AddComment(GetValidProjectComment());
        }

        return project;
    }

    public DomainEntity.Project GetValidProjectWithSkills(int total = 10)
    {
        var project = CreateValidProject();

        for (int i = 0; i < total; i++)
        {
            project.AddSkills(GetValidProjectSkill());
        }

        return project;
    }

    public List<DomainEntity.ProjectComment> GetListValidProjectComment(int total = 10)
    {
        var list = new List<DomainEntity.ProjectComment>();

        for (int i = 0; i < total; i++)
        {
            list.Add(GetValidProjectComment());
        }

        return list;
    }

    public List<DomainEntity.Models.ProjectSkills> GetListValidProjectSkills(int total = 10)
    {
        var list = new List<DomainEntity.Models.ProjectSkills>();

        for (int i = 0; i < total; i++)
        {
            list.Add(GetValidProjectSkill());
        }

        return list;
    }

}
