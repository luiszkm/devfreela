using DevFreela.API.ApiModels.Response;
using DevFreela.Application.UseCases.User.Common;
using DevFreela.E2ETest.Api.User.Common;
using FluentAssertions;
using System.Net;
using DevFreela.Application.UseCases.User.GetUser;
using DevFreela.Infrastructure.Persistence.Repository;
using Microsoft.AspNetCore.Mvc;
using DevFreela.Application.UseCases.Project.Common;
using DevFreela.Application.UseCases.Project.GetProject;

namespace DevFreela.E2ETest.Api.User;
[Collection(nameof(UserAPITestFixture))]
public class GetUserTest
{

    private readonly UserAPITestFixture _fixture;

    public GetUserTest(UserAPITestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(GetUser))]
    [Trait("E2E/API", "User/Get - Endpoints")]

    public async Task GetUser()
    {
        var (user, password) = await _fixture.GetUserInDataBase();
        var userAuthenticate = _fixture.ApiClient.AddAuthorizationHeader(user.Email, password);
        userAuthenticate.Result.Should().BeTrue();

        var inputModel = new GetUserInput(user.Id);

        var (response, output) = await _fixture
            .ApiClient.Get<ApiResponse
                <UserModelOutput>>($"/users/{user.Id}", inputModel);
        response!.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Should().NotBeNull();

        output!.Data.Should().NotBeNull();
        output!.Data!.Id.Should().NotBeEmpty();
        output!.Data!.Name.Should().Be(user.Name);
        output!.Data!.Email.Should().Be(user.Email);
        output!.Data!.BirthDate.Should().Be(user.BirthDate);

    }

    [Fact(DisplayName = nameof(GetUserWithSkills))]
    [Trait("E2E/API", "User/Get - Endpoints")]

    public async Task GetUserWithSkills()
    {
        var (user, password) = await _fixture.GetUserInDataBase(withSkills: true);
        var userAuthenticate = _fixture.ApiClient.AddAuthorizationHeader(user.Email, password);
        userAuthenticate.Result.Should().BeTrue();

        var inputModel = new GetUserInput(user.Id);

        var (response, output) = await _fixture
            .ApiClient.Get<ApiResponse
                <UserModelOutput>>($"/users/{user.Id}", inputModel);
        response!.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Should().NotBeNull();

        output!.Data.Should().NotBeNull();
        output!.Data!.Id.Should().NotBeEmpty();
        output!.Data!.Name.Should().Be(user.Name);
        output!.Data!.Email.Should().Be(user.Email);
        output!.Data!.BirthDate.Should().Be(user.BirthDate);

        output!.Data!.Skills.Should().NotBeNullOrEmpty();

    }
    [Fact(DisplayName = nameof(ThrowWhenNotFoundUser))]
    [Trait("E2E/API", "User/Get - Endpoints")]

    public async Task ThrowWhenNotFoundUser()
    {
        var (user, password) = await _fixture.GetUserInDataBase();
        var userAuthenticate = _fixture.ApiClient.AddAuthorizationHeader(user.Email, password);
        userAuthenticate.Result.Should().BeTrue();
        var invalidId = Guid.NewGuid();
        var inputModel = new GetUserInput(invalidId);

        var (response, output) = await _fixture
            .ApiClient.Get<ProblemDetails>($"/users/{invalidId}", inputModel);
        response!.StatusCode.Should().Be(HttpStatusCode.NotFound);
        response.Should().NotBeNull();


    }

    [Fact(DisplayName = nameof(NotThrowWhenUserIsNotLoggedInToGet))]
    [Trait("E2E/API", "Project/Update - Endpoints")]
    public async Task NotThrowWhenUserIsNotLoggedInToGet()
    {
        var (user, _) = await _fixture.GetUserInDataBase();

        var inputModel = new GetUserInput(user.Id);

        var (response, output) = await _fixture
            .ApiClient.Get<ApiResponse
                <UserModelOutput>>($"/users/{user.Id}", inputModel);
        response!.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Should().NotBeNull();

        output!.Data.Should().NotBeNull();
        output!.Data!.Id.Should().NotBeEmpty();
        output!.Data!.Name.Should().Be(user.Name);
        output!.Data!.Email.Should().Be(user.Email);
        output!.Data!.BirthDate.Should().Be(user.BirthDate);


    }

}
