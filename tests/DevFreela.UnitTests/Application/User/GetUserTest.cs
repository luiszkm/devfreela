

using DevFreela.Application.Exceptions;
using DevFreela.Application.UseCases.User.GetUser;
using DevFreela.UnitTests.Application.Common;
using Moq;

namespace DevFreela.UnitTests.Application.User;

[Collection(nameof(UserApplicationTestFixture))]
public class GetUserTest
{

    private readonly UserApplicationTestFixture _fixture;

    public GetUserTest(UserApplicationTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(GetUserApplication))]
    [Trait("Application", "Get - User")]
    public async Task GetUserApplication()
    {
        var repositoryMock = _fixture.GetUserRepositoryMock();
        var skillsRepositoryMock = _fixture.GetSkillsRepositoryMock();
        var user = _fixture.GetValidUser();

        repositoryMock.Setup(r =>
                r.GetById(user.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);


        var useCase = new UserUseCase.GetUser.GetUser(
            repositoryMock.Object,
            skillsRepositoryMock.Object);

        var input = new GetUserInput(user.Id);
        var output = await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(
            r => r.GetById(
                user.Id,
                It.IsAny<CancellationToken>()),
            Times.Once);

        output.Should().NotBeNull();
        output.Id.Should().Be(user.Id);
        output.Name.Should().Be(user.Name);
        output.Email.Should().Be(user.Email);
        output.BirthDate.Should().Be(user.BirthDate);

    }

    [Fact(DisplayName = nameof(GetUserWithSkills))]
    [Trait("Application", "Get - User")]
    public async Task GetUserWithSkills()
    {
        var repositoryMock = _fixture.GetUserRepositoryMock();
        var skillsRepositoryMock = _fixture.GetSkillsRepositoryMock();
        var skills = _fixture.GetValidSkill();
        var user = _fixture.GetValidUser();
        var userSkills = _fixture.GetValidUserSkills(user!.Id, skills!.Id);

        skillsRepositoryMock.Setup(s => s.GetById(It.IsAny<Guid>()))
            .Returns(skills);
        repositoryMock.Setup(r =>
                r.GetById(user.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        repositoryMock.Setup(r =>
                r.GetUserWithSkills(user.Id))
            .ReturnsAsync(userSkills);


        var useCase = new UserUseCase.GetUser.GetUser(
            repositoryMock.Object,
            skillsRepositoryMock.Object);

        var input = new GetUserInput(user.Id);
        var output = await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(
            r => r.GetById(
                user.Id,
                It.IsAny<CancellationToken>()),
            Times.Once);
        repositoryMock.Verify(
            r => r.GetUserWithSkills(
                user.Id),
            Times.Once);



        output.Should().NotBeNull();
        output.Id.Should().Be(user.Id);
        output.Name.Should().Be(user.Name);
        output.Email.Should().Be(user.Email);
        output.BirthDate.Should().Be(user.BirthDate);
        output.Skills.Should().NotBeNull();
        output.Skills.Should().HaveCount(userSkills.Count);

    }

    [Fact(DisplayName = nameof(ThrowWhenUserNotFound))]
    [Trait("Application", "Get - User")]
    public async Task ThrowWhenUserNotFound()
    {
        var repositoryMock = _fixture.GetUserRepositoryMock();
        var skillsRepositoryMock = _fixture.GetSkillsRepositoryMock();

        var invalidUserId = Guid.NewGuid();
        var user = _fixture.GetValidUser();

        repositoryMock.Setup(r =>
                r.GetById(user.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        var useCase = new UserUseCase.GetUser.GetUser(
            repositoryMock.Object,
            skillsRepositoryMock.Object);

        var input = new GetUserInput(invalidUserId);
        var action = async () =>
            await useCase.Handle(input, CancellationToken.None);

        await action.Should().ThrowAsync<NotFoundException>();



    }
}
