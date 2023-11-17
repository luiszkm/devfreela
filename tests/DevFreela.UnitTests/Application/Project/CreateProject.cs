

using DevFreela.Domain.Domain.Exceptions;
using DevFreela.UnitTests.Application.Common;
using Moq;

namespace DevFreela.UnitTests.Application.Project;

[Collection(nameof(ProjectApplicationTestFixture))]
public class CreateProject
{
    private readonly ProjectApplicationTestFixture _fixture;

    public CreateProject(ProjectApplicationTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(CreateProjectApplication))]
    [Trait("Application", "Create - Project")]

    public async Task CreateProjectApplication()
    {
        var repositoryMock = _fixture.GetProjectRepositoryMock();

        var useCase = new ProjectUseCase.CreateProject.CreateProject(
                       repositoryMock.Object);

        var input = _fixture.GetValidInputModel();

        var output = await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(r => r.Create(
            It.IsAny<DomainEntity.Project>(),
            CancellationToken.None), Times.Once);

        output.Should().NotBeNull();
        output.Id.Should().NotBeEmpty();
        output.Title.Should().Be(input.Title);
        output.Description.Should().Be(input.Description);
        output.TotalCost.Should().Be(input.TotalCost);
        output.ClientId.Should().Be(input.IdClient);
        output.CreatedAt.Should().BeBefore(DateTime.Now);

    }

    [Theory(DisplayName = nameof(ThrowWhenInvalidTitle))]
    [Trait("Application", "Create - Project")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("  ")]
    [InlineData("1234")]

    public async Task ThrowWhenInvalidTitle(string? title)
    {
        var repositoryMock = _fixture.GetProjectRepositoryMock();

        var useCase = new ProjectUseCase.CreateProject.CreateProject(
            repositoryMock.Object);

        var input = _fixture.GetValidInputModel();
        input.Title = title;
        var action = async () => await useCase.Handle(input, CancellationToken.None);
        action.Should().ThrowAsync<EntityValidationExceptions>();


    }

    [Theory(DisplayName = nameof(ThrowWhenInvalidDescription))]
    [Trait("Application", "Create - Project")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("  ")]
    [InlineData("123456789")]
    public async Task ThrowWhenInvalidDescription(string? description)
    {
        var repositoryMock = _fixture.GetProjectRepositoryMock();

        var useCase = new ProjectUseCase.CreateProject.CreateProject(
            repositoryMock.Object);

        var input = _fixture.GetValidInputModel();
        input.Description = description;
        var action = async () => await useCase.Handle(input, CancellationToken.None);
        action.Should().ThrowAsync<EntityValidationExceptions>();

    }

    [Theory(DisplayName = nameof(ThrowWhenInvalidTotalCoat))]
    [Trait("Application", "Create - Project")]
    [InlineData(null)]
    [InlineData(0)]
    [InlineData(-100)]

    public async Task ThrowWhenInvalidTotalCoat(decimal totalCoast)
    {
        var repositoryMock = _fixture.GetProjectRepositoryMock();

        var useCase = new ProjectUseCase.CreateProject.CreateProject(
            repositoryMock.Object);

        var input = _fixture.GetValidInputModel();
        input.TotalCost = totalCoast;
        var action = async () => await useCase.Handle(input, CancellationToken.None);
        action.Should().ThrowAsync<EntityValidationExceptions>();

    }
    [Fact(DisplayName = nameof(ThrowWhenInvalidClientId))]
    [Trait("Application", "Create - Project")]


    public async Task ThrowWhenInvalidClientId()
    {
        var repositoryMock = _fixture.GetProjectRepositoryMock();

        var useCase = new ProjectUseCase.CreateProject.CreateProject(
            repositoryMock.Object);

        var input = _fixture.GetValidInputModel();
        input.IdClient = Guid.Empty;
        var action = async () => await useCase.Handle(input, CancellationToken.None);
        action.Should().ThrowAsync<EntityValidationExceptions>();


    }
}
