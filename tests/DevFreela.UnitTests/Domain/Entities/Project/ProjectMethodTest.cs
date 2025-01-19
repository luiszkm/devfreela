
using DevFreela.Domain.Domain.Enums;

namespace DevFreela.UnitTests.Domain.Entities.Project;

[Collection(nameof(ProjectTestFixture))]

public class ProjectMethodTest
{
    private readonly ProjectTestFixture _fixture;

    public ProjectMethodTest(ProjectTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(ListFreelancersInterested))]
    [Trait("Domain", "Methods - Project")]

    public void ListFreelancersInterested()
    {
        var project = _fixture.CreateValidProject();
        var user = _fixture.GetListUsers();
        project.AddFreelancersInterested(user[8]);
        project.FreelancersInterested.Should().HaveCount(1);

        project.AddFreelancersInterested(user[9]);
        project.FreelancersInterested.Should().HaveCount(2);


    }
    [Fact(DisplayName = nameof(RemoveFreelancersInterested))]
    [Trait("Domain", "Methods - Project")]

    public void RemoveFreelancersInterested()
    {
        var users = _fixture.GetListUsers();

        var project = _fixture.GetValidProjectWithFreelancerInterested();
        project.AddFreelancersInterested(users[8]);
        project.AddFreelancersInterested(users[9]);

        project.FreelancersInterested.Should().HaveCount(12);

        var projectRemoved = project.FreelancersInterested.Find(x => x.Id == users[8].Id);

        project.RemoveFreelancersInterested(users[8]);
        project.FreelancersInterested.Should().HaveCount(11);
        project.FreelancersInterested.Should().NotContain(projectRemoved);


        var arrayInt = new int[] { 2, 7 };
        var t = project.EncontrarIndices(arrayInt, 9);





    }

    [Fact(DisplayName = nameof(ContractFreelancer))]
    [Trait("Domain", "Methods - Project")]

    public void ContractFreelancer()
    {
        var users = _fixture.GetListUsers();
        var project = _fixture.GetValidProjectWithFreelancerInterested();
        project.AddFreelancersInterested(users[8]);

        project.ContractFreelancer(users[8].Id);

        project.FreelancersInterested.Should().HaveCount(11);
        project.IdFreelancer.Should().Be(users[8].Id);
        project.Status.Should().Be(ProjectStatusEnum.InProgress);
        project.StartedAt.Should().BeBefore(DateTime.Now);

    }

    [Fact(DisplayName = nameof(RemoveComment))]
    [Trait("Domain", "Methods - Project")]

    public void RemoveComment()
    {
        var comments = _fixture.GetListValidProjectComment();
        var project = _fixture.GetValidProjectWithComments();
        project.AddComment(comments[8]);
        project.AddComment(comments[9]);
        project.Comments.Should().HaveCount(12);

        var commentRemoved = project.Comments.Find(x => x.Id == comments[8].Id);
        project.RemoveComment(comments[8]);
        project.Comments.Should().HaveCount(11);
        project.Comments.Should().NotContain(commentRemoved);

    }
    [Fact(DisplayName = nameof(AddComment))]
    [Trait("Domain", "Methods - Project")]

    public void AddComment()
    {
        var comments = _fixture.GetListValidProjectComment();
        var project = _fixture.GetValidProjectWithComments();
        project.AddComment(comments[8]);
        project.AddComment(comments[9]);
        project.Comments.Should().HaveCount(12);

    }

    [Fact(DisplayName = nameof(RemoveSkills))]
    [Trait("Domain", "Methods - Project")]

    public void RemoveSkills()
    {
        var skills = _fixture.GetListValidProjectSkills();
        var project = _fixture.GetValidProjectWithSkills();
        project.AddSkills(skills[9]);
        project.AddSkills(skills[8]);
        project.Skills.Should().HaveCount(12);
        var skillRemoved = project.Skills.Find(x => x.Id == skills[9].Id);
        project.RemoveSkills(skills[9]);
        project.Skills.Should().HaveCount(11);
        project.Skills.Should().NotContain(skillRemoved);

    }
    [Fact(DisplayName = nameof(AddSkills))]
    [Trait("Domain", "Methods - Project")]

    public void AddSkills()
    {
        var skills = _fixture.GetListValidProjectSkills();
        var project = _fixture.GetValidProjectWithSkills();
        project.AddSkills(skills[9]);
        project.AddSkills(skills[8]);
        project.Skills.Should().HaveCount(12);

    }




}
