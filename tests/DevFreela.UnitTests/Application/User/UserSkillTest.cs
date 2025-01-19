

using DevFreela.Application.UseCases.User.UserSkill;
using DevFreela.UnitTests.Application.Common;
using Moq;

namespace DevFreela.UnitTests.Application.User;

[Collection(nameof(UserApplicationTestFixture))]
public class UserSkillTest
{
    private readonly UserApplicationTestFixture _fixture;

    public UserSkillTest(UserApplicationTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(AddSkill))]
    [Trait("Application ", "skill - User")]

    public async Task AddSkill()
    {
        var repositoryMock = _fixture.GetUserRepositoryMock();
        var user = _fixture.GetValidUser();
        var skillList = _fixture.GetValidSkillList();
        repositoryMock.Setup(r =>
                r.GetById(user.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        var useCase = new UserUseCase.UserSkill.UserSkill(repositoryMock.Object);

        var input = new UserSkillInput(user.Id, skillList);

        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Id.Should().Be(user.Id);
        output.Skills.Should().HaveCount(skillList.Count);

    }
}
