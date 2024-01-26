using DevFreela.API.ApiModels.Response;
using DevFreela.Application.UseCases.Project.Common;
using DevFreela.E2ETest.Api.Project.Common;
using FluentAssertions;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.E2ETest.Api.Project;
[Collection(nameof(ProjectAPITestFixture))]
public class UpdateProjectTest
{
    private readonly ProjectAPITestFixture _fixture;

    public UpdateProjectTest(ProjectAPITestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(UpdateProject))]
    [Trait("E2E/API", "Project/Update - Endpoints")]

    public async Task UpdateProject()
    {
        var (user, password) = await _fixture.GetUserInDataBase();
        var userAuthenticate = _fixture.ApiClient.AddAuthorizationHeader(user.Email, password);
        var project = await _fixture.CreateProjectInDataBase(user.Id);

        userAuthenticate.Result.Should().BeTrue();

        var inputModel = _fixture.GetUpdateProjectInputModel(project.Id);

        var (response, output) = await _fixture
            .ApiClient.Put<ApiResponse<ProjectModelOutput>>($"/projects/{project.Id}", inputModel);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Should().NotBeNull();
        output!.Data.Should().NotBeNull();
        output!.Data!.Id.Should().NotBeEmpty();
        output!.Data!.Title.Should().Be(inputModel.Title);
        output!.Data!.Description.Should().Be(inputModel.Description);
        output!.Data!.TotalCost.Should().Be(inputModel.TotalCost);
        output!.Data.ClientId.Should().Be(user.Id);


    }

    [Fact(DisplayName = nameof(ThrowWhenProjectNotFound))]
    [Trait("E2E/API", "Project/Update - Endpoints")]

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

    [Theory(DisplayName = nameof(ThrowWhenTitleIsInvalid))]
    [Trait("E2E/API", "Project/Update - Endpoints")]
    [InlineData("aaa")]

    public async Task ThrowWhenTitleIsInvalid(string title)
    {
        var (user, password) = await _fixture.GetUserInDataBase();
        var userAuthenticate = _fixture.ApiClient.AddAuthorizationHeader(user.Email, password);
        var project = await _fixture.CreateProjectInDataBase(user.Id);

        userAuthenticate.Result.Should().BeTrue();
        var inputModel = _fixture.GetUpdateProjectInputModel(project.Id);
        inputModel.Title = title;

        var (response, output) = await _fixture
            .ApiClient.Put<ProblemDetails>($"/projects/{project.Id}", inputModel);

        response!.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        response.Should().NotBeNull();
        output!.Title.Should().Be("One or more validation errors occurred.");
        output!.Status.Should().Be((int)HttpStatusCode.UnprocessableEntity);

    }
    [Theory(DisplayName = nameof(ThrowWhenDescriptionIsInvalid))]
    [Trait("E2E/API", "Project/Update - Endpoints")]
    [InlineData("aaa")]

    public async Task ThrowWhenDescriptionIsInvalid(string desc)
    {
        var (user, password) = await _fixture.GetUserInDataBase();
        var userAuthenticate = _fixture.ApiClient.AddAuthorizationHeader(user.Email, password);
        var project = await _fixture.CreateProjectInDataBase(user.Id);

        userAuthenticate.Result.Should().BeTrue();
        var inputModel = _fixture.GetUpdateProjectInputModel(project.Id);
        inputModel.Description = desc;

        var (response, output) = await _fixture
            .ApiClient.Put<ProblemDetails>($"/projects/{project.Id}", inputModel);

        response!.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        response.Should().NotBeNull();
        output!.Title.Should().Be("One or more validation errors occurred.");
        output!.Status.Should().Be((int)HttpStatusCode.UnprocessableEntity);


    }

    [Theory(DisplayName = nameof(ThrowWhenTotalCoastIsInvalid))]
    [Trait("E2E/API", "Project/Update - Endpoints")]
    [InlineData(-11)]

    public async Task ThrowWhenTotalCoastIsInvalid(decimal totalCoast)
    {
        var (user, password) = await _fixture.GetUserInDataBase();
        var userAuthenticate = _fixture.ApiClient.AddAuthorizationHeader(user.Email, password);
        var project = await _fixture.CreateProjectInDataBase(user.Id);

        userAuthenticate.Result.Should().BeTrue();
        var inputModel = _fixture.GetUpdateProjectInputModel(project.Id);
        inputModel.TotalCost = totalCoast;

        var (response, output) = await _fixture
            .ApiClient.Put<ProblemDetails>($"/projects/{project.Id}", inputModel);

        response!.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        response.Should().NotBeNull();
        output!.Title.Should().Be("One or more validation errors occurred.");
        output!.Status.Should().Be((int)HttpStatusCode.UnprocessableEntity);


    }

    [Fact(DisplayName = nameof(ThrowWhenUserIsNotLoggedInToUpdated))]
    [Trait("E2E/API", "Project/Update - Endpoints")]
    public async Task ThrowWhenUserIsNotLoggedInToUpdated()
    {
        var (user, _) = await _fixture.GetUserInDataBase();
        var project = await _fixture.CreateProjectInDataBase(user.Id);
        var inputModel = _fixture.GetUpdateProjectInputModel(project.Id);

        _fixture.ApiClient.RemoveAuthorizationHeader();


        var (response, _) = await _fixture
            .ApiClient.Put<ApiResponse<ProjectModelOutput>>($"/projects/{project.Id}", inputModel);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        response.Should().NotBeNull();

    }
}
