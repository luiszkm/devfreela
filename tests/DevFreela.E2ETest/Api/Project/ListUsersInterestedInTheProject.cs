using System.Net;
using DevFreela.API.ApiModels.Response;
using DevFreela.Application.UseCases.Project.FreelancersInterested;
using DevFreela.Application.UseCases.Project.Common;
using DevFreela.E2ETest.Api.Project.Common;
using FluentAssertions;


namespace DevFreela.E2ETest.Api.Project;

[Collection(nameof(ProjectAPITestFixture))]
public class ListUsersInterestedInTheProject
{
    private readonly ProjectAPITestFixture _fixture;

    public ListUsersInterestedInTheProject(ProjectAPITestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(AddUserOnTheLisInterested))]
    [Trait("E2E/API", "Project/AddUserOnTheLisInterested - Endpoints")]

    public async Task AddUserOnTheLisInterested()
    {
        var ownerUser = await _fixture.GetUserInDataBase();
        var (user, password) = await _fixture.GetUserInDataBase();
        var userAuthenticate = _fixture.ApiClient.AddAuthorizationHeader(user.Email, password);
        var project = await _fixture.CreateProjectInDataBase(ownerUser.user.Id);
        userAuthenticate.Result.Should().BeTrue();

        var inputModel = new FreelancersInterestedInput(project.Id, user.Id);

        var (response, output) = await _fixture
            .ApiClient.Patch<ApiResponse<ProjectModelOutput>>($"/projects/{project.Id}/interested", inputModel);

        response!.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Should().NotBeNull();
        output!.Data.Should().NotBeNull();
        output!.Data!.Id.Should().NotBeEmpty();
        output!.Data!.Id.Should().Be(project.Id);
        output!.Data!.Title.Should().Be(project.Title);
        output!.Data!.FreelancersInterested.Should().NotBeEmpty();
        output!.Data!.FreelancersInterested.Should().HaveCount(1);
        output!.Data!.FreelancersInterested.First().FreelancerId.Should().Be(user.Id);

    }


    [Fact(DisplayName = nameof(AddUserOnTheLisInterestedWithMayUser))]
    [Trait("E2E/API", "Project/AddUserOnTheLisInterested - Endpoints")]

    public async Task AddUserOnTheLisInterestedWithMayUser()
    {
        var ownerUser = await _fixture.GetUserInDataBase();
        var (user, password) = await _fixture.GetUserInDataBase();
        var userAuthenticate = _fixture.ApiClient.AddAuthorizationHeader(user.Email, password);
        var project = await _fixture.CreateProjectInDataBase(ownerUser.user.Id, true);
        userAuthenticate.Result.Should().BeTrue();

        var inputModel = new FreelancersInterestedInput(project.Id, user.Id);

        var (response, output) = await _fixture
            .ApiClient.Patch<ApiResponse<ProjectModelOutput>>($"/projects/{project.Id}/interested", inputModel);

        var totalFreelancers = project.FreelancersInterested.Count + 1;

        response!.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Should().NotBeNull();
        output!.Data.Should().NotBeNull();
        output!.Data!.Id.Should().NotBeEmpty();
        output!.Data!.Id.Should().Be(project.Id);
        output!.Data!.Title.Should().Be(project.Title);
        output!.Data!.FreelancersInterested.Should().NotBeEmpty();
        output!.Data!.FreelancersInterested.Should().HaveCount(totalFreelancers);
        output!.Data!.FreelancersInterested.Should().Contain(f => f.FreelancerId == user.Id);


    }
    [Fact(DisplayName = nameof(RemoveUserOnTheLisInterestedWithMayUser))]
    [Trait("E2E/API", "Project/AddUserOnTheLisInterested - Endpoints")]

    public async Task RemoveUserOnTheLisInterestedWithMayUser()
    {
        var ownerUser = await _fixture.GetUserInDataBase();
        var password = _fixture.GetValidPassword();
        var user = _fixture.GetValidUser(password: password);
        var project = await _fixture.CreateProjectInDataBase(ownerUser.user.Id, true, user);
        var userAuthenticate = _fixture.ApiClient.AddAuthorizationHeader(user.Email, password);
        userAuthenticate.Result.Should().BeTrue();

        var inputModel = new FreelancersInterestedInput(project.Id, user.Id, false);

        var (response, output) = await _fixture
            .ApiClient.Patch<ApiResponse<ProjectModelOutput>>($"/projects/{project.Id}/interested", inputModel);

        var totalFreelancers = project.FreelancersInterested.Count - 1;

        response!.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Should().NotBeNull();
        output!.Data.Should().NotBeNull();
        output!.Data!.Id.Should().NotBeEmpty();
        output!.Data!.Id.Should().Be(project.Id);
        output!.Data!.Title.Should().Be(project.Title);
        output!.Data!.FreelancersInterested.Should().NotBeEmpty();
        output!.Data!.FreelancersInterested.Should().HaveCount(totalFreelancers);
        output!.Data!.FreelancersInterested.Should().NotContain(f => f.FreelancerId == user.Id);


    }

    [Fact(DisplayName = nameof(RemoveUserOnTheLisInterested))]
    [Trait("E2E/API", "Project/AddUserOnTheLisInterested - Endpoints")]

    public async Task RemoveUserOnTheLisInterested()
    {
        var ownerUser = await _fixture.GetUserInDataBase();
        var password = _fixture.GetValidPassword();
        var user = _fixture.GetValidUser(password: password);
        var project = await _fixture.CreateProjectInDataBase(ownerUser.user.Id, false, user);
        var userAuthenticate = _fixture.ApiClient.AddAuthorizationHeader(user.Email, password);
        userAuthenticate.Result.Should().BeTrue();

        var inputModel = new FreelancersInterestedInput(project.Id, user.Id, false);

        var (response, output) = await _fixture
            .ApiClient.Patch<ApiResponse<ProjectModelOutput>>($"/projects/{project.Id}/interested", inputModel);

        response!.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Should().NotBeNull();
        output!.Data.Should().NotBeNull();
        output!.Data!.Id.Should().NotBeEmpty();
        output!.Data!.Id.Should().Be(project.Id);
        output!.Data!.Title.Should().Be(project.Title);
        output!.Data!.FreelancersInterested.Should().BeEmpty();
        output!.Data!.FreelancersInterested.Should().HaveCount(0);
        output!.Data!.FreelancersInterested.Should().NotContain(f => f.FreelancerId == user.Id);


    }

    [Fact(DisplayName = nameof(ListFreelancerInterestedInTheProject))]
    [Trait("E2E/API", "Project/AddUserOnTheLisInterested - Endpoints")]

    public async Task ListFreelancerInterestedInTheProject()
    {
        var ownerUser = await _fixture.GetUserInDataBase();
        var project = await _fixture.CreateProjectInDataBase(
            ownerUser.user.Id,
            true);// 5 freelancers interested

        var (response, output) = await _fixture
            .ApiClient.Get<ApiResponse<ProjectModelOutput>>($"/projects/{project.Id}");

        response!.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Should().NotBeNull();
        output!.Data.Should().NotBeNull();
        output!.Data!.Id.Should().NotBeEmpty();
        output!.Data!.Id.Should().Be(project.Id);
        output!.Data!.FreelancersInterested.Should().NotBeEmpty();
        output!.Data!.FreelancersInterested.Should().HaveCount(5);

    }

}
