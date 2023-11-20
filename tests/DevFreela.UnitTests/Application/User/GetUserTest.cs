

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

    [Fact(DisplayName = nameof(UpdateUserApplication))]
    [Trait("Application", "Update - User")]
    public async Task UpdateUserApplication()
    {
        var repositoryMock = _fixture.GetUserRepositoryMock();
        var user = _fixture.GetValidUser();

        repositoryMock.Setup(r =>
                r.GetById(user.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        var useCase = new UserUseCase.GetUser.GetUser(
            repositoryMock.Object);

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

    [Fact(DisplayName = nameof(ThrowWhenUserNotFound))]
    [Trait("Application", "Update - User")]
    public async Task ThrowWhenUserNotFound()
    {
        var repositoryMock = _fixture.GetUserRepositoryMock();
        var invalidUserId = Guid.NewGuid();
        var user = _fixture.GetValidUser();

        repositoryMock.Setup(r =>
                r.GetById(user.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        var useCase = new UserUseCase.GetUser.GetUser(
            repositoryMock.Object);

        var input = new GetUserInput(invalidUserId);
        var action = async () =>
            await useCase.Handle(input, CancellationToken.None);

        await action.Should().ThrowAsync<NotFoundException>();



    }
}
