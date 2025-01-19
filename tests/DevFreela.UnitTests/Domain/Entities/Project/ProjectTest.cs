
using DevFreela.Domain.Domain.Enums;
using DevFreela.Domain.Domain.Exceptions;

namespace DevFreela.UnitTests.Domain.Entities.Project;


[Collection(nameof(ProjectTestFixture))]
public class ProjectTest
{
    private readonly ProjectTestFixture _fixture;

    public ProjectTest(ProjectTestFixture fixture)
    {
        _fixture = fixture;
    }


    [Fact(DisplayName = nameof(InstantiateProject))]
    [Trait("Domain", "Project")]

    public void InstantiateProject()
    {
        var fixture = _fixture.CreateValidProject();
        var project = new DomainEntity.Project(
                       fixture.Title,
                       fixture.Description,
                       fixture.TotalCost,
                       fixture.IdClient);

        project.Should().NotBeNull();
        project.Id.Should().NotBeEmpty();
        project.Title.Should().Be(fixture.Title);
        project.Description.Should().Be(fixture.Description);
        project.TotalCost.Should().Be(fixture.TotalCost);
        project.IdClient.Should().Be(fixture.IdClient);
        project.CreatedAt.Should().BeBefore(DateTime.Now);
        project.Status.Should().Be(ProjectStatusEnum.Created);
        project.Comments.Should().BeEmpty();
        project.Skills.Should().BeEmpty();
    }

    [Fact(DisplayName = nameof(UpdateProject))]
    [Trait("Domain", "Project")]
    public void UpdateProject()
    {
        var project = _fixture.CreateValidProject();
        var newTitle = _fixture.GetValidTitle();
        var newDescription = _fixture.GetValidDescription();
        var newTotalCost = _fixture.GetRandomDecimal();

        project.Update(
            title: newTitle,
            description: newDescription,
            totalCost: newTotalCost);


        project.Should().NotBeNull();
        project.Id.Should().NotBeEmpty();
        project.Title.Should().Be(newTitle);
        project.Description.Should().Be(newDescription);
        project.TotalCost.Should().Be(newTotalCost);
        project.CreatedAt.Should().BeBefore(DateTime.Now);
        project.Status.Should().Be(ProjectStatusEnum.Created);
        project.Comments.Should().BeEmpty();
        project.Skills.Should().BeEmpty();
    }

    [Fact(DisplayName = nameof(UpdateProjectOnlyTotalCost))]
    [Trait("Domain", "Project")]
    public void UpdateProjectOnlyTotalCost()
    {
        var project = _fixture.CreateValidProject();
        var newTotalCost = _fixture.GetRandomDecimal();

        project.Update(totalCost: newTotalCost);

        project.Should().NotBeNull();
        project.Id.Should().NotBeEmpty();
        project.Title.Should().Be(project.Title);
        project.Description.Should().Be(project.Description);
        project.TotalCost.Should().Be(newTotalCost);
        project.CreatedAt.Should().BeBefore(DateTime.Now);
        project.Status.Should().Be(ProjectStatusEnum.Created);
        project.Comments.Should().BeEmpty();
        project.Skills.Should().BeEmpty();
    }

    [Fact(DisplayName = nameof(UpdateProjectOnlyDescription))]
    [Trait("Domain", "Project")]
    public void UpdateProjectOnlyDescription()
    {
        var project = _fixture.CreateValidProject();
        var newDescription = _fixture.GetValidDescription();
        project.Update(description: newDescription);

        project.Should().NotBeNull();
        project.Id.Should().NotBeEmpty();
        project.Title.Should().Be(project.Title);
        project.Description.Should().Be(newDescription);
        project.TotalCost.Should().Be(project.TotalCost);
        project.CreatedAt.Should().BeBefore(DateTime.Now);
        project.Status.Should().Be(ProjectStatusEnum.Created);
        project.Comments.Should().BeEmpty();
        project.Skills.Should().BeEmpty();
    }

    [Fact(DisplayName = nameof(UpdateProjectOnlyTitle))]
    [Trait("Domain", "Project")]
    public void UpdateProjectOnlyTitle()
    {
        var project = _fixture.CreateValidProject();
        var newTitle = _fixture.GetValidTitle();


        project.Update(
            title: newTitle);

        project.Should().NotBeNull();
        project.Id.Should().NotBeEmpty();
        project.Title.Should().Be(newTitle);
        project.Description.Should().Be(project.Description);
        project.TotalCost.Should().Be(project.TotalCost);
        project.CreatedAt.Should().BeBefore(DateTime.Now);
        project.Status.Should().Be(ProjectStatusEnum.Created);
        project.Comments.Should().BeEmpty();
        project.Skills.Should().BeEmpty();
    }

    [Theory(DisplayName = nameof(ChangeInitialProjectStatus))]
    [Trait("Domain", "Project")]
    [InlineData(ProjectStatusEnum.InProgress)]
    [InlineData(ProjectStatusEnum.Suspended)]
    public void ChangeInitialProjectStatus(ProjectStatusEnum newStatus)
    {
        var project = _fixture.CreateValidProject();
        project.ChangeStatus(newStatus);

        project.Should().NotBeNull();
        project.Status.Should().Be(newStatus);
    }

    [Fact(DisplayName = nameof(ChangeProjectStatus))]
    [Trait("Domain", "Project")]
    public void ChangeProjectStatus()
    {
        var project = _fixture.CreateValidProject();
        project.ChangeStatus(ProjectStatusEnum.InProgress);
        project.ChangeStatus(ProjectStatusEnum.PaymentPending);

        project.Should().NotBeNull();
        project.Status.Should().Be(ProjectStatusEnum.PaymentPending);

        project.ChangeStatus(ProjectStatusEnum.Finished);
        project.Status.Should().Be(ProjectStatusEnum.Finished);

    }

    [Theory(DisplayName = nameof(ThrowWhenDataIsInvalidForInstantiate))]
    [Trait("Domain", "Project")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("aaaa")]
    public void ThrowWhenDataIsInvalidForInstantiate(string newTitle)
    {
        var fixture = _fixture.CreateValidProject();
        var action = new Action(() => new DomainEntity.Project(
                       title: newTitle,
                       description: fixture.Description,
                       totalCost: fixture.TotalCost,
                       idClient: _fixture.GetValidIdClient()));
        action.Should().NotBeNull();
        action.Should().Throw<EntityValidationExceptions>();
    }


    [Theory(DisplayName = nameof(ThrowWhenDescriptionIsInvalidForInstantiate))]
    [Trait("Domain", "Project")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("123456789")]
    public void ThrowWhenDescriptionIsInvalidForInstantiate(string newDescription)
    {
        var fixture = _fixture.CreateValidProject();
        var action = new Action(() => new DomainEntity.Project(
            title: fixture.Title,
            description: newDescription,
            totalCost: fixture.TotalCost,
            idClient: _fixture.GetValidIdClient()));
        action.Should().NotBeNull();
        action.Should().Throw<EntityValidationExceptions>();
    }

    [Fact(DisplayName = nameof(ThrowWhenTotalCostIsInvalidForInstantiate))]
    [Trait("Domain", "Project")]

    public void ThrowWhenTotalCostIsInvalidForInstantiate()
    {
        var fixture = _fixture.CreateValidProject();

        var action = new Action(() => new DomainEntity.Project(
                       title: fixture.Title,
                                  description: fixture.Description,
                                  totalCost: _fixture.GetInvalidTotalCost(),
                                  idClient: _fixture.GetValidIdClient()));
        action.Should().NotBeNull();
        action.Should().Throw<EntityValidationExceptions>();
    }

    [Fact(DisplayName = nameof(ThrowWhenIdClientIsInvalidForInstantiate))]
    [Trait("Domain", "Project")]

    public void ThrowWhenIdClientIsInvalidForInstantiate()
    {
        var fixture = _fixture.CreateValidProject();

        var action = new Action(() => new DomainEntity.Project(
                                  title: fixture.Title,
                                  description: fixture.Description,
                                  totalCost: fixture.TotalCost,
                                  idClient: Guid.Empty));
        action.Should().NotBeNull();
        action.Should().Throw<EntityValidationExceptions>();
    }

    [Theory(DisplayName = nameof(ThrowWhenUpdateWithInvalidTitle))]
    [Trait("Domain", "Project")]
    [InlineData("")]
    [InlineData("aaaa")]
    public void ThrowWhenUpdateWithInvalidTitle(string newTitle)
    {
        var project = _fixture.CreateValidProject();
        var action = () => project.Update(title: newTitle);
        action.Should().NotBeNull();
        action.Should().Throw<EntityValidationExceptions>();
    }
    [Theory(DisplayName = nameof(ThrowWhenUpdateWithInvalidDescription))]
    [Trait("Domain", "Project")]
    [InlineData("")]
    [InlineData("123456789")]
    public void ThrowWhenUpdateWithInvalidDescription(string newDescription)
    {
        var project = _fixture.CreateValidProject();
        var action = () => project.Update(description: newDescription);
        action.Should().NotBeNull();
        action.Should().Throw<EntityValidationExceptions>();
    }

    [Fact(DisplayName = nameof(ThrowWhenUpdateWithInvalidTotalCost))]
    [Trait("Domain", "Project")]
    public void ThrowWhenUpdateWithInvalidTotalCost()
    {
        var project = _fixture.CreateValidProject();
        var action = () => project.Update(totalCost: _fixture.GetInvalidTotalCost());
        action.Should().NotBeNull();
        action.Should().Throw<EntityValidationExceptions>();
    }


}
