

using DevFreela.Application.UseCases.Project.ChangeStatus;
using DevFreela.Domain.Domain.Enums;
using DevFreela.UnitTests.Application.Common;
using Moq;

namespace DevFreela.UnitTests.Application.Project;
[Collection(nameof(ProjectApplicationTestFixture))]
public class ChangeStatusTest
{

    private readonly ProjectApplicationTestFixture _fixture;

    public ChangeStatusTest(ProjectApplicationTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(ChangeStatusToInprogress))]
    [Trait("Application", "ChangeStatus - Project")]


    public async Task ChangeStatusToInprogress()
    {
        var project = _fixture.CreateValidProject();
        var repositoryMock = _fixture.GetProjectRepositoryMock();
        repositoryMock.Setup(p => p.GetById(project.Id, default))
            .ReturnsAsync(project);

        var useCase = new ProjectUseCase.ChangeStatus.ChangeStatus(
                                  repositoryMock.Object);

        var inputModel = new ProjectUseCase.ChangeStatus
            .ChangeStatusInputModel(
                project.Id,
                ChangeStatusInputModel.ProjectStatusEnum.InProgress);

        await useCase.Handle(inputModel, new CancellationToken());
        project.Status.Should().Be(ProjectStatusEnum.InProgress);

    }


    [Fact(DisplayName = nameof(ChangeStatusToCanceled))]
    [Trait("Application", "ChangeStatus - Project")]
    public async Task ChangeStatusToCanceled()
    {
        var project = _fixture.CreateValidProject();
        var repositoryMock = _fixture.GetProjectRepositoryMock();
        repositoryMock.Setup(p => p.GetById(project.Id, default))
            .ReturnsAsync(project);
        project.ChangeStatus(ProjectStatusEnum.Suspended);

        var useCase = new ProjectUseCase.ChangeStatus.ChangeStatus(
            repositoryMock.Object);

        var inputModel = new ProjectUseCase.ChangeStatus
            .ChangeStatusInputModel(
                project.Id,
                ChangeStatusInputModel.ProjectStatusEnum.Cancelled);

        await useCase.Handle(inputModel, new CancellationToken());
        project.Status.Should().Be(ProjectStatusEnum.Cancelled);

    }
    [Fact(DisplayName = nameof(ChangeStatusToSuspended))]
    [Trait("Application", "ChangeStatus - Project")]
    public async Task ChangeStatusToSuspended()
    {
        var project = _fixture.CreateValidProject();
        var repositoryMock = _fixture.GetProjectRepositoryMock();
        repositoryMock.Setup(p => p.GetById(project.Id, default))
            .ReturnsAsync(project);

        var useCase = new ProjectUseCase.ChangeStatus.ChangeStatus(
            repositoryMock.Object);

        var inputModel = new ProjectUseCase.ChangeStatus
            .ChangeStatusInputModel(
                project.Id,
                ChangeStatusInputModel.ProjectStatusEnum.Suspended);

        await useCase.Handle(inputModel, new CancellationToken());
        project.Status.Should().Be(ProjectStatusEnum.Suspended);

    }
    [Fact(DisplayName = nameof(ChangeStatusToFinished))]
    [Trait("Application", "ChangeStatus - Project")]
    public async Task ChangeStatusToFinished()
    {
        var project = _fixture.CreateValidProject();
        var repositoryMock = _fixture.GetProjectRepositoryMock();
        repositoryMock.Setup(p => p.GetById(project.Id, default))
            .ReturnsAsync(project);
        project.ChangeStatus(ProjectStatusEnum.InProgress);
        project.ChangeStatus(ProjectStatusEnum.PaymentPending);

        var useCase = new ProjectUseCase.ChangeStatus.ChangeStatus(
            repositoryMock.Object);

        var inputModel = new ProjectUseCase.ChangeStatus
            .ChangeStatusInputModel(
                project.Id,
                ChangeStatusInputModel.ProjectStatusEnum.Finished);

        await useCase.Handle(inputModel, new CancellationToken());
        project.Status.Should().Be(ProjectStatusEnum.Finished);

    }
}
