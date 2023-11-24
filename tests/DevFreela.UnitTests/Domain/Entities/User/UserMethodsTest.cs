using System.Runtime.InteropServices;
using DevFreela.UnitTests.Domain.Entities.Project;


namespace DevFreela.UnitTests.Domain.Entities.User;

[Collection(nameof(UserTestFixture))]
public class UserMethodsTest
{

    private readonly UserTestFixture _fixture;

    public UserMethodsTest(UserTestFixture fixture)
    {
        _fixture = fixture;
    }


    [Fact(DisplayName = nameof(AddSkills))]
    [Trait("Domain", "Methods - User")]

    public void AddSkills()
    {
        var user = _fixture.CreateValidUser();
        var skills = _fixture.GetValidSkillList();

        user.AddSkills(skills);
        user.Skills.Should().HaveCount(skills.Count);
        user.Skills.Should().NotBeEmpty();
        user.Skills.Should().NotBeNull();
    }




    [Fact(DisplayName = nameof(AddOwnedProject))]
    [Trait("Domain", "Methods - User")]

    public void AddOwnedProject()
    {
        var user = _fixture.CreateValidUser();
        var project = _fixture.CreateValidProject();
        user.AddOwnedProject(project);

        user.OwnedProjects.Should().Contain(project);
        user.OwnedProjects.Should().HaveCount(1);
        user.OwnedProjects.Should().NotBeEmpty();
        user.OwnedProjects.Should().NotBeNull();

    }


    [Fact(DisplayName = nameof(AddFreelanceProjects))]
    [Trait("Domain", "Methods - User")]

    public void AddFreelanceProjects()
    {
        var user = _fixture.CreateValidUser();
        var project = _fixture.CreateValidProject();
        user.AddFreelanceProject(project);

        user.FreelanceProjects.Should().Contain(project);
        user.FreelanceProjects.Should().HaveCount(1);
        user.FreelanceProjects.Should().NotBeEmpty();
        user.FreelanceProjects.Should().NotBeNull();

    }



    [Fact(DisplayName = nameof(AddProjectComment))]
    [Trait("Domain", "Methods - User")]

    public void AddProjectComment()
    {
        var user = _fixture.CreateValidUser();
        var projectId = Guid.NewGuid();
        var comment = _fixture.GetValidProjectComment(user.Id, projectId);

        user.AddComment(comment);

        user.Comments.Should().Contain(comment);
        user.Comments.Should().HaveCount(1);
        user.Comments.Should().NotBeEmpty();
        user.Comments.Should().NotBeNull();


    }

    [Fact(DisplayName = nameof(RemoveProjectComment))]
    [Trait("Domain", "Methods - User")]

    public void RemoveProjectComment()
    {
        var user = _fixture.CreateValidUser();
        var projectId = Guid.NewGuid();
        var comment = _fixture.GetValidProjectComment(user.Id, projectId);

        user.AddComment(comment);
        user.RemoveComment(comment);

        user.Comments.Should().NotContain(comment);
        user.Comments.Should().HaveCount(0);
        user.Comments.Should().BeEmpty();

    }
}
