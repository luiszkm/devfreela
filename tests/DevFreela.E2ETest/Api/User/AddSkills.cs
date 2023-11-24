using System.Net;
using DevFreela.API.ApiModels.Response;
using DevFreela.Application.UseCases.User.Common;
using DevFreela.Application.UseCases.User.UserSkill;
using DevFreela.E2ETest.Api.User.Common;
using FluentAssertions;


namespace DevFreela.E2ETest.Api.User;
[Collection(nameof(UserAPITestFixture))]
public class AddSkills
{
    private readonly UserAPITestFixture _fixture;

    public AddSkills(UserAPITestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(InsertSkill))]
    [Trait("E2E/API", "User/Create - Endpoints")]

    public async Task InsertSkill()
    {
        var skillList = _fixture.GetValidSkillList();
        var (user, password) = await _fixture.GetUserInDataBase();
        var userAuthenticate = _fixture.ApiClient.AddAuthorizationHeader(user.Email, password);
        userAuthenticate.Result.Should().BeTrue();

        var inputModel = new UserSkillInput(user.Id,
            skillList);

        var (response, output) = await _fixture
            .ApiClient.Post<ApiResponse
                           <UserModelOutput>>("/users/skills", inputModel);
        response!.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Should().NotBeNull();

        output!.Data.Should().NotBeNull();
        output!.Data!.Skills.Should().NotBeNullOrEmpty();
        output!.Data!.Skills.Should().HaveCount(skillList.Count);

    }
}
