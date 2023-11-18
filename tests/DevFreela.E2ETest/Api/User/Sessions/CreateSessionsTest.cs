using System.Net;
using DevFreela.API.ApiModels.Response;
using DevFreela.Application.UseCases.Session;
using DevFreela.E2ETest.Api.User.Common;
using FluentAssertions;

namespace DevFreela.E2ETest.Api.User.Sessions;

[Collection(nameof(UserAPITestFixture))]
public class CreateSessionsTest
{
    private readonly UserAPITestFixture _fixture;

    public CreateSessionsTest(UserAPITestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(CreateSessionAPI))]
    [Trait("E2E/API", "User/Create - Endpoints")]

    public async Task CreateSessionAPI()
    {
        var password = _fixture.GetValidPassword();
        var dbContext = _fixture.CreateApiDbContextInMemory();
        var user = _fixture.GetValidUser(password: password);
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();

        var inputModel = new SessionInput(
            user.Email,
            password
        );
        var (response, output) = await _fixture
            .ApiClient.Post<ApiResponse
                           <SessionOutput>>("/session", inputModel);


        response!.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Should().NotBeNull();

        output!.Data.Should().NotBeNull();
        output!.Data.UserId.Should().Be(user.Id);
        output!.Data.Token.Should().NotBeEmpty();

    }

    [Fact(DisplayName = nameof(ThrowWhenUserNotExists))]
    [Trait("E2E/API", "User/Create - Endpoints")]

    public async Task ThrowWhenUserNotExists()
    {
        var user = _fixture.GetValidUser();


        var inputModel = new SessionInput(
            user.Email,
            user.Password
        );
        var (response, output) = await _fixture
            .ApiClient.Post<ApiResponse
                <SessionOutput>>("/session", inputModel);

        response!.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        response.Should().NotBeNull();
    }

    [Fact(DisplayName = nameof(ThrowWhenPasswordIsInvalid))]
    [Trait("E2E/API", "User/Create - Endpoints")]

    public async Task ThrowWhenPasswordIsInvalid()
    {
        var dbContext = _fixture.CreateApiDbContextInMemory();
        var user = _fixture.GetValidUser();
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();

        var inputModel = new SessionInput(
            user.Email,
            "invalid"
        );
        var (response, output) = await _fixture
            .ApiClient.Post<ApiResponse
                <SessionOutput>>("/session", inputModel);

        response!.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        response.Should().NotBeNull();
    }


    [Fact(DisplayName = nameof(ThrowWhenEmailIsInvalid))]
    [Trait("E2E/API", "User/Create - Endpoints")]

    public async Task ThrowWhenEmailIsInvalid()
    {
        var dbContext = _fixture.CreateApiDbContextInMemory();
        var user = _fixture.GetValidUser();
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();

        var inputModel = new SessionInput(
            "invalid-email",
            user.Password

        );
        var (response, output) = await _fixture
            .ApiClient.Post<ApiResponse
                <SessionOutput>>("/session", inputModel);

        response!.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        response.Should().NotBeNull();
    }
}
