using DevFreela.API.ApiModels.Response;
using DevFreela.Application.UseCases.User.Common;
using DevFreela.E2ETest.Api.User.Common;
using System.Net;
using DevFreela.Application.UseCases.Project.UpdateProject;
using DevFreela.Application.UseCases.User.UpdatePassword;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;


namespace DevFreela.E2ETest.Api.User;

[Collection(nameof(UserAPITestFixture))]
public class UpdatePassword
{

    private readonly UserAPITestFixture _fixture;

    public UpdatePassword(UserAPITestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(UpdateUserPassword))]
    [Trait("E2E/API", "User/Patch - Endpoints")]

    public async Task UpdateUserPassword()
    {
        var (user, password) = await _fixture.GetUserInDataBase();
        var userAuthenticate = _fixture.ApiClient.AddAuthorizationHeader(user.Email, password);
        userAuthenticate.Result.Should().BeTrue();
        var newPassword = "DevFreela@2024";

        var passwordHashOld = _fixture.GetPasswordHash(password);
        var passwordHashNew = _fixture.GetPasswordHash(newPassword);

        var inputModel = new UpdatePasswordInput(
            user.Id,
            password,
            newPassword
            );

        var (response, _) = await _fixture
            .ApiClient.Patch<ApiResponse
                <NoContent>>($"/users/{user.Id}/password", inputModel);
        response!.StatusCode.Should().Be(HttpStatusCode.NoContent);
        response.Should().NotBeNull();

        var persistence = await _fixture.Persistence.GetById(user.Id);

        persistence!.Password.Should().NotBe(passwordHashOld);
        persistence.Password.Should().Be(passwordHashNew);

    }

    [Fact(DisplayName = nameof(UpdateUserPasswordAndAuthenticate))]
    [Trait("E2E/API", "User/Patch - Endpoints")]

    public async Task UpdateUserPasswordAndAuthenticate()
    {
        var (user, password) = await _fixture.GetUserInDataBase();
        var userAuthenticate = _fixture.ApiClient.AddAuthorizationHeader(user.Email, password);
        userAuthenticate.Result.Should().BeTrue();
        var newPassword = _fixture.GetInvalidPassword();
        var inputModel = new UpdatePasswordInput(
            user.Id,
            password,
            newPassword
        );

        var (response, _) = await _fixture
            .ApiClient.Patch<ApiResponse
                <NoContent>>($"/users/{user.Id}/password", inputModel);
        response!.StatusCode.Should().Be(HttpStatusCode.NoContent);
        response.Should().NotBeNull();

        _fixture.ApiClient.RemoveAuthorizationHeader();
        var userAuthenticateNewPassword = _fixture.ApiClient.AddAuthorizationHeader(user.Email, newPassword);
        userAuthenticateNewPassword.Result.Should().BeTrue();

    }

    [Fact(DisplayName = nameof(UpdateUserPasswordAndNotAuthenticate))]
    [Trait("E2E/API", "User/Patch - Endpoints")]

    public async Task UpdateUserPasswordAndNotAuthenticate()
    {
        var (user, password) = await _fixture.GetUserInDataBase();
        var userAuthenticate = _fixture.ApiClient.AddAuthorizationHeader(user.Email, password);
        userAuthenticate.Result.Should().BeTrue();
        var newPassword = _fixture.GetInvalidPassword();
        var inputModel = new UpdatePasswordInput(
            user.Id,
            password,
            newPassword
        );

        var (response, _) = await _fixture
            .ApiClient.Patch<ApiResponse
                <NoContent>>($"/users/{user.Id}/password", inputModel);
        response!.StatusCode.Should().Be(HttpStatusCode.NoContent);
        response.Should().NotBeNull();

        _fixture.ApiClient.RemoveAuthorizationHeader();
        var userAuthenticateNewPassword = _fixture.ApiClient.AddAuthorizationHeader(user.Email, password);
        userAuthenticateNewPassword.Result.Should().BeFalse();

    }

    [Fact(DisplayName = nameof(ThrowWhenNewPasswordIsSameCurrent))]
    [Trait("E2E/API", "User/Patch - Endpoints")]

    public async Task ThrowWhenNewPasswordIsSameCurrent()
    {
        var (user, password) = await _fixture.GetUserInDataBase();
        var userAuthenticate = _fixture.ApiClient.AddAuthorizationHeader(user.Email, password);
        userAuthenticate.Result.Should().BeTrue();

        var inputModel = new UpdatePasswordInput(
            user.Id,
            user.Password,
            user.Password
            );

        var (response, _) = await _fixture
            .ApiClient.Patch<ApiResponse
                <ProblemDetails>>($"/users/{user.Id}/password", inputModel);
        response!.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        response.Should().NotBeNull();
    }

    [Fact(DisplayName = nameof(ThrowWhenNewUserNotFound))]
    [Trait("E2E/API", "User/Patch - Endpoints")]
    public async Task ThrowWhenNewUserNotFound()
    {
        var (user, password) = await _fixture.GetUserInDataBase();
        var userAuthenticate = _fixture.ApiClient.AddAuthorizationHeader(user.Email, password);
        var exampleGuid = Guid.NewGuid();
        userAuthenticate.Result.Should().BeTrue();

        var inputModel = new UpdatePasswordInput(
            exampleGuid,
            user.Password,
            user.Password
        );

        var (response, _) = await _fixture
            .ApiClient.Patch<ApiResponse
                <ProblemDetails>>($"/users/{exampleGuid}/password", inputModel);
        response!.StatusCode.Should().Be(HttpStatusCode.NotFound);
        response.Should().NotBeNull();
    }

    [Fact(DisplayName = nameof(ThrowWhenNewUserIsNotLogged))]
    [Trait("E2E/API", "User/Patch - Endpoints")]

    public async Task ThrowWhenNewUserIsNotLogged()
    {
        var (user, _) = await _fixture.GetUserInDataBase();
        _fixture.ApiClient.RemoveAuthorizationHeader();
        var inputModel = new UpdatePasswordInput(
            user.Id,
            user.Password,
            user.Password
        );

        var (response, _) = await _fixture
            .ApiClient.Patch<ApiResponse
                <ProblemDetails>>($"/users/{user.Id}/password", inputModel);
        response!.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        response.Should().NotBeNull();


    }
}
