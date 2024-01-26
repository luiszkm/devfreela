

using DevFreela.API.ApiModels.Response;
using DevFreela.Application.UseCases.Project.Common;
using DevFreela.Application.UseCases.Project.GetProject;
using DevFreela.E2ETest.Api.Project.Common;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using FluentAssertions;
using DevFreela.Application.UseCases.Project.DeleteProject;

namespace DevFreela.E2ETest.Api.Project;

[Collection(nameof(ProjectAPITestFixture))]
public class DeleteProjectTest
{

    private readonly ProjectAPITestFixture _fixture;

    public DeleteProjectTest(ProjectAPITestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(DeleteProject))]
    [Trait("E2E/API", "Project/Delete - Endpoints")]

    public async Task DeleteProject()
    {
        var (user, password) = await _fixture.GetUserInDataBase();
        var userAuthenticate = _fixture.ApiClient.AddAuthorizationHeader(user.Email, password);
        var project = await _fixture.CreateProjectInDataBase(user.Id);

        userAuthenticate.Result.Should().BeTrue();

        var (response, _) = await _fixture
            .ApiClient.Delete<NoContentResult>($"/projects/{project.Id}");

        response!.StatusCode.Should().Be(HttpStatusCode.NoContent);
        response.Should().NotBeNull();

    }

    [Fact(DisplayName = nameof(ThrowWhenProjectNotFound))]
    [Trait("E2E/API", "Project/Delete - Endpoints")]

    public async Task ThrowWhenProjectNotFound()
    {
        var (user, password) = await _fixture.GetUserInDataBase();
        var userAuthenticate = _fixture.ApiClient.AddAuthorizationHeader(user.Email, password);
        var projectId = Guid.NewGuid();

        userAuthenticate.Result.Should().BeTrue();

        var inputModel = _fixture.GetUpdateProjectInputModel(projectId);

        var (response, output) = await _fixture
            .ApiClient.Delete<ProblemDetails>($"/projects/{projectId}");

        response!.StatusCode.Should().Be(HttpStatusCode.NotFound);
        response.Should().NotBeNull();
        output!.Title.Should().Be("Aggregate NotFound");
        output!.Status.Should().Be((int)HttpStatusCode.NotFound);
    }

    [Fact(DisplayName = nameof(ThrowWhenUserIsNotLoggedInToGet))]
    [Trait("E2E/API", "Project/Delete - Endpoints")]
    public async Task ThrowWhenUserIsNotLoggedInToGet()
    {
        var (user, _) = await _fixture.GetUserInDataBase();
        var project = await _fixture.CreateProjectInDataBase(user.Id);
        _fixture.ApiClient.RemoveAuthorizationHeader();

        var (response, _) = await _fixture
            .ApiClient.Delete<ProblemDetails>($"/projects/{project.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        response.Should().NotBeNull();


    }
}
