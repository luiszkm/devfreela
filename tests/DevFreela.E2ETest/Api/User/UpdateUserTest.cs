

using DevFreela.API.ApiModels.Response;
using DevFreela.Application.UseCases.User.Common;
using DevFreela.E2ETest.Api.User.Common;
using System.Net;
using DevFreela.Application.UseCases.User.UpdateUser;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.E2ETest.Api.User;

[Collection(nameof(UserAPITestFixture))]
public class UpdateUserTest
{
    private readonly UserAPITestFixture _fixture;

    public UpdateUserTest(UserAPITestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(UpdateUser))]
    [Trait("E2E/API", "User/Put - Endpoints")]

    public async Task UpdateUser()
    {
        var (user, password) = await _fixture.GetUserInDataBase();
        var userAuthenticate = _fixture.ApiClient.AddAuthorizationHeader(user.Email, password);
        userAuthenticate.Result.Should().BeTrue();

        var inputModel = _fixture.GetUpdateUserInput(user.Id, user.Active);

        var (response, output) = await _fixture
            .ApiClient.Put<ApiResponse
                <UserModelOutput>>($"/users/{user.Id}", inputModel);
        response!.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Should().NotBeNull();

        output!.Data.Should().NotBeNull();
        output!.Data!.Id.Should().NotBeEmpty();
        output!.Data!.Name.Should().Be(inputModel.Name);
        output!.Data!.Email.Should().Be(inputModel.Email);
        output!.Data!.BirthDate.Should().Be(inputModel.BirthDate);
    }

    [Theory(DisplayName = nameof(UpdateUser))]
    [Trait("E2E/API", "User/Put - Endpoints")]
    [MemberData(
        nameof(UserApiTestDataGenerator.GetInvalidUpdateInputs),
        MemberType = typeof(UserApiTestDataGenerator)
    )]

    public async Task ThrowWhenInvalidData(UpdateUserInput inputModel)
    {
        var (user, password) = await _fixture.GetUserInDataBase();
        var userAuthenticate = _fixture.ApiClient.AddAuthorizationHeader(user.Email, password);
        userAuthenticate.Result.Should().BeTrue();
        inputModel.Id = user.Id;

        var (response, output) = await _fixture
            .ApiClient.Put<ProblemDetails>($"/users/{user.Id}", inputModel);
        response!.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        response.Should().NotBeNull();
        output!.Title.Should().Be("One or more validation errors occurred.");
        output!.Status.Should().Be((int)HttpStatusCode.UnprocessableEntity);

    }

    [Fact(DisplayName = nameof(ThrowWhenUserNotFound))]
    [Trait("E2E/API", "User/Put - Endpoints")]


    public async Task ThrowWhenUserNotFound()
    {
        var (user, password) = await _fixture.GetUserInDataBase();
        var userAuthenticate = _fixture.ApiClient.AddAuthorizationHeader(user.Email, password);
        userAuthenticate.Result.Should().BeTrue();
        var userId = Guid.NewGuid();
        var inputModel = _fixture.GetUpdateUserInput(userId, user.Active);

        var (response, output) = await _fixture
            .ApiClient.Put<ProblemDetails>($"/users/{userId}", inputModel);
        response!.StatusCode.Should().Be(HttpStatusCode.NotFound);
        response.Should().NotBeNull();
        output!.Title.Should().Be("Aggregate NotFound");
        output!.Status.Should().Be((int)HttpStatusCode.NotFound);

    }

    [Fact(DisplayName = nameof(NotThrowWhenUserIsNotLoggedInToGet))]
    [Trait("E2E/API", "Project/Update - Endpoints")]
    public async Task NotThrowWhenUserIsNotLoggedInToGet()
    {
        var (user, _) = await _fixture.GetUserInDataBase();

        _fixture.ApiClient.RemoveAuthorizationHeader();

        var inputModel = _fixture.GetUpdateUserInput(user.Id, user.Active);

        var (response, _) = await _fixture
            .ApiClient.Put<ApiResponse
                <UserModelOutput>>($"/users/{user.Id}", inputModel);

        response!.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        response.Should().NotBeNull();

    }
}
