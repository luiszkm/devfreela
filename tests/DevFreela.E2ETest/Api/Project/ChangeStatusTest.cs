


using DevFreela.E2ETest.Api.Project.Common;
using FluentAssertions;
using System.Net;
using DevFreela.Application.UseCases.Project.ChangeStatus;
using DevFreela.Domain.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.E2ETest.Api.Project;
[Collection(nameof(ProjectAPITestFixture))]
public class ChangeStatusTest
{

    private readonly ProjectAPITestFixture _fixture;

    public ChangeStatusTest(ProjectAPITestFixture fixture)
    {
        _fixture = fixture;
    }

    [Theory(DisplayName = nameof(ChangeStatus))]
    [Trait("E2E/API", "Project/ChangeStatus - Endpoints")]
    [InlineData(ChangeStatusInputModel.ProjectStatusEnum.InProgress)]
    [InlineData(ChangeStatusInputModel.ProjectStatusEnum.Suspended)]
    public async Task ChangeStatus(ChangeStatusInputModel.ProjectStatusEnum status)
    {
        var dbContext = _fixture.CreateApiDbContextInMemory();
        var (user, password) = await _fixture.GetUserInDataBase();
        var userAuthenticate = _fixture.ApiClient.AddAuthorizationHeader(user.Email, password);
        var project = await _fixture.CreateProjectInDataBase(user.Id);

        userAuthenticate.Result.Should().BeTrue();

        var inputModel = new ChangeStatusInputModel(project.Id, status);

        var (response, _) = await _fixture
            .ApiClient.Patch<NoContentResult>($"/projects/{project.Id}", inputModel);
        var projectChanged = await dbContext.Projects.FindAsync(project.Id);

        response!.StatusCode.Should().Be(HttpStatusCode.NoContent);
        response.Should().NotBeNull();

        projectChanged.Should().NotBeNull();
        projectChanged!.Status.Should().Be((ProjectStatusEnum)status);



    }
    [Fact(DisplayName = nameof(ChangeStatusToCanceled))]
    [Trait("E2E/API", "Project/ChangeStatus - Endpoints")]

    public async Task ChangeStatusToCanceled()
    {
        var dbContext = _fixture.CreateApiDbContextInMemory();
        var (user, password) = await _fixture.GetUserInDataBase();
        var userAuthenticate = _fixture.ApiClient.AddAuthorizationHeader(user.Email, password);
        var project = await _fixture.CreateProjectInDataBase(user.Id);
        project.ChangeStatus(ProjectStatusEnum.Suspended);
        dbContext.Projects.Update(project);
        await dbContext.SaveChangesAsync();


        userAuthenticate.Result.Should().BeTrue();

        var inputModel = new ChangeStatusInputModel(project.Id,
            ChangeStatusInputModel.ProjectStatusEnum.Cancelled);

        var (response, _) = await _fixture
            .ApiClient.Patch<NoContentResult>($"/projects/{project.Id}", inputModel);

        var projectChanged = await dbContext.Projects
            .AsNoTracking().FirstOrDefaultAsync(p => p.Id == project.Id);

        projectChanged.Should().NotBeNull();
        projectChanged!.Status.Should().Be(ProjectStatusEnum.Cancelled);

        response!.StatusCode.Should().Be(HttpStatusCode.NoContent);
        response.Should().NotBeNull();

    }

    [Fact(DisplayName = nameof(ChangeStatusToFinished))]
    [Trait("E2E/API", "Project/ChangeStatus - Endpoints")]

    public async Task ChangeStatusToFinished()
    {
        var dbContext = _fixture.CreateApiDbContextInMemory();
        var (user, password) = await _fixture.GetUserInDataBase();
        var userAuthenticate = _fixture.ApiClient.AddAuthorizationHeader(user.Email, password);
        var project = await _fixture.CreateProjectInDataBase(user.Id);
        project.ChangeStatus(ProjectStatusEnum.InProgress);
        dbContext.Projects.Update(project);


        project.ChangeStatus(ProjectStatusEnum.PaymentPending);
        dbContext.Projects.Update(project);

        await dbContext.SaveChangesAsync();

        userAuthenticate.Result.Should().BeTrue();

        var inputModel = new ChangeStatusInputModel(project.Id,
            ChangeStatusInputModel.ProjectStatusEnum.Finished);

        var (response, _) = await _fixture
            .ApiClient.Patch<NoContentResult>($"/projects/{project.Id}", inputModel);

        var projectChanged = await dbContext.Projects
            .AsNoTracking().FirstOrDefaultAsync(p => p.Id == project.Id);

        projectChanged.Should().NotBeNull();
        projectChanged!.Status.Should().Be(ProjectStatusEnum.Finished);

        response!.StatusCode.Should().Be(HttpStatusCode.NoContent);
        response.Should().NotBeNull();

    }

    [Fact(DisplayName = nameof(ThrowWhenProjectNotFound))]
    [Trait("E2E/API", "Project/ChangeStatus - Endpoints")]

    public async Task ThrowWhenProjectNotFound()
    {
        var (user, password) = await _fixture.GetUserInDataBase();
        var userAuthenticate = _fixture.ApiClient.AddAuthorizationHeader(user.Email, password);
        var projectId = Guid.NewGuid();

        userAuthenticate.Result.Should().BeTrue();

        var inputModel = new ChangeStatusInputModel(projectId,
            ChangeStatusInputModel.ProjectStatusEnum.InProgress);

        var (response, output) = await _fixture
            .ApiClient.Patch<ProblemDetails>($"/projects/{projectId}", inputModel);

        response!.StatusCode.Should().Be(HttpStatusCode.NotFound);
        response.Should().NotBeNull();
        output!.Title.Should().Be("Aggregate NotFound");
        output!.Status.Should().Be((int)HttpStatusCode.NotFound);
    }

    [Fact(DisplayName = nameof(ThrowWhenUserIsNotLoggedInToGet))]
    [Trait("E2E/API", "Project/ChangeStatus - Endpoints")]
    public async Task ThrowWhenUserIsNotLoggedInToGet()
    {
        var (user, _) = await _fixture.GetUserInDataBase();
        var project = await _fixture.CreateProjectInDataBase(user.Id);

        _fixture.ApiClient.RemoveAuthorizationHeader();

        var inputModel = new ChangeStatusInputModel(project.Id,
            ChangeStatusInputModel.ProjectStatusEnum.InProgress);

        var (response, _) = await _fixture
            .ApiClient.Patch<ProblemDetails>($"/projects/{project.Id}", inputModel);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        response.Should().NotBeNull();


    }
}
