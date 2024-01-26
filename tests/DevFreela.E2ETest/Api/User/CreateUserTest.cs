

using DevFreela.API.ApiModels.Response;
using DevFreela.Application.UseCases.User.Common;
using DevFreela.E2ETest.Api.User.Common;
using FluentAssertions;
using System.Net;
using DevFreela.Application.UseCases.User.CreateUser;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.UnitTests.Api.User;

[Collection(nameof(UserAPITestFixture))]
public class CreateUserTest
{
    private readonly UserAPITestFixture _fixture;

    public CreateUserTest(UserAPITestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(CreateUserAPI))]
    [Trait("E2E/API", "User/Create - Endpoints")]

    public async Task CreateUserAPI()
    {
        var inputModel = _fixture.GetUserInput();

        var (response, output) = await _fixture
            .ApiClient.Post<ApiResponse
                <UserModelOutput>>("/users", inputModel);
        response!.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Should().NotBeNull();

        output!.Data.Should().NotBeNull();
        output!.Data!.Id.Should().NotBeEmpty();
        output!.Data!.Name.Should().Be(inputModel.Name);
        output!.Data!.Email.Should().Be(inputModel.Email);

    }

    [Theory(DisplayName = nameof(ThrowWhenCantInstantiateAggregate))]
    [Trait("E2E/API", "User/Create - Endpoints")]
    [MemberData(
        nameof(UserApiTestDataGenerator.GetInvalidInputs),
        MemberType = typeof(UserApiTestDataGenerator)
    )]

    public async Task ThrowWhenCantInstantiateAggregate(CreateUserInput input)
    {
        var (response, output) = await _fixture
            .ApiClient.Post<ProblemDetails>(
                "/users", input);

        response!.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        response.Should().NotBeNull();
        output!.Title.Should().Be("One or more validation errors occurred.");
        output!.Status.Should().Be((int)HttpStatusCode.UnprocessableEntity);
    }





}
