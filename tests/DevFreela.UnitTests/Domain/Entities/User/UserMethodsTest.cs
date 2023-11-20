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
        var skill = _fixture.CreateValidSkill();

        user.AddSkill(skill);

        user.Skills.Should().Contain(skill);
        user.Skills.Should().HaveCount(1);
        user.Skills.Should().NotBeEmpty();
        user.Skills.Should().NotBeNull();
    }

    [Fact(DisplayName = nameof(RemoveSkills))]
    [Trait("Domain", "Methods - User")]

    public void RemoveSkills()
    {
        var user = _fixture.CreateValidUser();
        var skill = _fixture.CreateValidSkill();
        user.AddSkill(skill);

        user.RemoveSkill(skill);

        user.Skills.Should().NotContain(skill);
        user.Skills.Should().HaveCount(0);
        user.Skills.Should().BeEmpty();
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

    [Fact(DisplayName = nameof(RemoveOwnedProject))]
    [Trait("Domain", "Methods - User")]

    public void RemoveOwnedProject()
    {
        var user = _fixture.CreateValidUser();
        var project = _fixture.CreateValidProject();
        user.AddOwnedProject(project);

        user.RemoveOwnedProject(project);

        user.OwnedProjects.Should().NotContain(project);
        user.OwnedProjects.Should().HaveCount(0);
        user.OwnedProjects.Should().BeEmpty();

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

    [Fact(DisplayName = nameof(RemoveFreelanceProjects))]
    [Trait("Domain", "Methods - User")]

    public void RemoveFreelanceProjects()
    {
        var user = _fixture.CreateValidUser();
        var project = _fixture.CreateValidProject();
        user.AddFreelanceProject(project);

        user.RemoveFreelanceProject(project);

        user.FreelanceProjects.Should().NotContain(project);
        user.FreelanceProjects.Should().HaveCount(0);
        user.FreelanceProjects.Should().BeEmpty();

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
