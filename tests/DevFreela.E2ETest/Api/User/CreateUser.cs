

using DevFreela.API.ApiModels.Response;
using DevFreela.Application.UseCases.User.Common;
using DevFreela.UnitTests.Api.User.Common;

namespace DevFreela.UnitTests.Api.User;

[Collection(nameof(UserAPITestFixture))]
public class CreateUser
{
    private readonly UserAPITestFixture _fixture;

    public CreateUser(UserAPITestFixture fixture)
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
                <UserModelOutput>>("/user", inputModel);
    }
}
