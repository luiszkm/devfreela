

using System.Net;
using DevFreela.API.ApiModels.Response;
using DevFreela.Application.UseCases.Project.Common;
using DevFreela.Domain.Domain.Enums;
using DevFreela.E2ETest.Api.Project.Common;
using FluentAssertions;

namespace DevFreela.E2ETest.Api.Project;
[Collection(nameof(CreateProjectTestFixture))]
public class CreateProjectTest
{
    private readonly CreateProjectTestFixture _fixture;
    public CreateProjectTest(CreateProjectTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(CreateProjectApi))]
    [Trait("E2E/API", "Project/Create - Endpoints")]

    public async Task CreateProjectApi()
    {
        var (user, password) = await _fixture.GetUserInDataBase();
        var userAuthenticate = _fixture.ApiClient.AddAuthorizationHeader(user.Email, password);


        userAuthenticate.Result.Should().BeTrue();
        var inputModel = _fixture.GetProjectInputModel(user.Id);

        var (response, output) = await _fixture
            .ApiClient.Post<ApiResponse<ProjectModelOutput>>("/projects", inputModel);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Should().NotBeNull();
        output!.Data.Should().NotBeNull();
        output!.Data!.Id.Should().NotBeEmpty();
        output!.Data!.Title.Should().Be(inputModel.Title);
        output!.Data!.Description.Should().Be(inputModel.Description);
        output!.Data!.TotalCost.Should().Be(inputModel.TotalCost);
        output!.Data.ClientId.Should().Be(user.Id);

    }


    [Fact(DisplayName = nameof(ThrowWhenUserIsNotLoggedIn))]
    [Trait("E2E/API", "Project/Create - Endpoints")]
    public async Task ThrowWhenUserIsNotLoggedIn()
    {

        var inputModel = _fixture.GetProjectInputModel();
        _fixture.ApiClient.RemoveAuthorizationHeader();


        var (response, _) = await _fixture
            .ApiClient.Post<UnauthorizedAccessException>("/projects", inputModel);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        response.Should().NotBeNull();

    }

    [Fact(DisplayName = nameof(ThrowWhenUserIsNotRoleClient))]
    [Trait("E2E/API", "Project/Create - Endpoints")]
    public async Task ThrowWhenUserIsNotRoleClient()
    {
        var password = _fixture.GetValidPassword();
        var dbContext = _fixture.CreateApiDbContextInMemory();
        var user = _fixture.GetValidUser(
            password: password,
            role: UserRole.Freelancer);
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();

        var userAuthenticate = _fixture.ApiClient.AddAuthorizationHeader(user.Email, password);

        userAuthenticate.Result.Should().BeTrue();
        var inputModel = _fixture.GetProjectInputModel(user.Id);

        var (response, output) = await _fixture
            .ApiClient.Post<UnauthorizedAccessException>("/projects", inputModel);

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        response.Should().NotBeNull();

    }

}
