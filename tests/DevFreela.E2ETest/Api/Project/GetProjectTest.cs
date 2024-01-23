using DevFreela.API.ApiModels.Response;
using DevFreela.Application.UseCases.Project.Common;
using DevFreela.E2ETest.Api.Project.Common;

using System.Net;
using DevFreela.Application.UseCases.Project.GetProject;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.E2ETest.Api.Project;

[Collection(nameof(ProjectAPITestFixture))]
public class GetProjectTest
{

    private readonly ProjectAPITestFixture _fixture;

    public GetProjectTest(ProjectAPITestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(GetProject))]
    [Trait("E2E/API", "Project/Get - Endpoints")]

    public async Task GetProject()
    {
        var (user, password) = await _fixture.GetUserInDataBase();
        var userAuthenticate = _fixture.ApiClient.AddAuthorizationHeader(user.Email, password);
        var project = await _fixture.CreateProjectInDataBase(user.Id);

        userAuthenticate.Result.Should().BeTrue();

        var inputModel = new GetProjectInput(project.Id);

        var (response, output) = await _fixture
            .ApiClient.Get<ApiResponse<ProjectModelOutput>>($"/projects/{project.Id}", inputModel);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Should().NotBeNull();
        output!.Data.Should().NotBeNull();
        output!.Data!.Id.Should().NotBeEmpty();
        output!.Data!.Id.Should().Be(project.Id);
        output!.Data!.Title.Should().Be(project.Title);
        output!.Data!.Description.Should().Be(project.Description);
        output!.Data.TotalCost.Should().Be(project.TotalCost);

    }

    [Fact(DisplayName = nameof(ThrowWhenProjectNotFound))]
    [Trait("E2E/API", "Project/Get - Endpoints")]

    public async Task ThrowWhenProjectNotFound()
    {
        var (user, password) = await _fixture.GetUserInDataBase();
        var userAuthenticate = _fixture.ApiClient.AddAuthorizationHeader(user.Email, password);
        var projectId = Guid.NewGuid();

        userAuthenticate.Result.Should().BeTrue();

        var inputModel = _fixture.GetUpdateProjectInputModel(projectId);

        var (response, output) = await _fixture
            .ApiClient.Put<ProblemDetails>($"/projects/{projectId}", inputModel);

        response!.StatusCode.Should().Be(HttpStatusCode.NotFound);
        response.Should().NotBeNull();
        output!.Title.Should().Be("Aggregate NotFound");
        output!.Status.Should().Be((int)HttpStatusCode.NotFound);
    }

    [Fact(DisplayName = nameof(NotThrowWhenUserIsNotLoggedInToGet))]
    [Trait("E2E/API", "Project/Update - Endpoints")]
    public async Task NotThrowWhenUserIsNotLoggedInToGet()
    {
        var (user, password) = await _fixture.GetUserInDataBase();
        var project = await _fixture.CreateProjectInDataBase(user.Id);
        var inputModel = new GetProjectInput(project.Id);

        var (response, output) = await _fixture
            .ApiClient.Get<ApiResponse<ProjectModelOutput>>($"/projects/{project.Id}", inputModel);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Should().NotBeNull();
        output!.Data.Should().NotBeNull();
        output!.Data!.Id.Should().NotBeEmpty();
        output!.Data!.Id.Should().Be(project.Id);
        output!.Data!.Title.Should().Be(project.Title);
        output!.Data!.Description.Should().Be(project.Description);
        output!.Data.TotalCost.Should().Be(project.TotalCost);

    }
}
