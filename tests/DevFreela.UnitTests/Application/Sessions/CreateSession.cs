

using DevFreela.Application.Exceptions;
using DevFreela.Domain.Domain.Enums;
using DevFreela.UnitTests.Application.Common;
using Moq;

namespace DevFreela.UnitTests.Application.Sessions;

[Collection(nameof(UserApplicationTestFixture))]
public class CreateSession
{
    private readonly UserApplicationTestFixture _fixture;
    public CreateSession(UserApplicationTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(CreateSessionApplication))]
    [Trait("Application", "Create - Session")]

    public async Task CreateSessionApplication()
    {
        var user = _fixture.GetValidUser();
        var repositoryMock = _fixture.GetUserRepositoryMock();
        var authMock = _fixture.GetAuthRepositoryMock();

        repositoryMock.Setup(u => u.GetUserByEmail(It.IsAny<string>()))
            .ReturnsAsync((string email) => user);


        var useCase = new SessionUseCase.Session(
            repositoryMock.Object,
            authMock.Object);

        var input = new SessionUseCase.SessionInput(
                       user.Email,
                       user.Password);

        var output = await useCase.Handle(input, CancellationToken.None);

        authMock.Verify(
            r => r.GenerateToken(
                It.IsAny<Guid>(),
                It.IsAny<UserRole>()),
            Times.Once);

        output.Should().NotBeNull();
        output.Token.Should().NotBeEmpty();
        output.USerID.Should().NotBeEmpty();
        output.USerID.Should().Be(user.Id);
    }

    [Fact(DisplayName = nameof(ThrowWhenEmailInvalid))]
    [Trait("Application", "Create - Session")]

    public async Task ThrowWhenEmailInvalid()
    {
        var email = _fixture.GetValidEmail();
        var user = _fixture.GetValidUser();
        var repositoryMock = _fixture.GetUserRepositoryMockWithUser(email);
        var authMock = _fixture.GetAuthRepositoryMock();
        repositoryMock.Setup(r => r.GetUserByEmail(user.Email))
            .ReturnsAsync(_fixture.GetValidUser(user.Email));

        var useCase = new SessionUseCase.Session(
            repositoryMock.Object,
            authMock.Object);

        var input = new SessionUseCase.SessionInput(
            email,
            user.Password);

        var action = async () =>
            await useCase.Handle(input, CancellationToken.None);

        action.Should().ThrowAsync<CredentialInvalid>();

        repositoryMock.Verify(
            r => r.GetUserByEmail(
                It.IsAny<string>()),
            Times.Once);
    }

    [Fact(DisplayName = nameof(ThrowWhenPasswordInvalid))]
    [Trait("Application", "Create - Session")]
    public async Task ThrowWhenPasswordInvalid()
    {
        var password = _fixture.GetValidPassword();
        var user = _fixture.GetValidUser();
        var repositoryMock = _fixture.GetUserRepositoryMockWithUser(user.Email);
        var authMock = _fixture.GetAuthRepositoryMock();
        repositoryMock.Setup(r => r.GetUserByEmail(user.Email))
            .ReturnsAsync(_fixture.GetValidUser(user.Email));

        var useCase = new SessionUseCase.Session(
            repositoryMock.Object,
            authMock.Object);

        var input = new SessionUseCase.SessionInput(
            user.Email,
            password);

        var action = async () =>
            await useCase.Handle(input, CancellationToken.None);

        action.Should().ThrowAsync<CredentialInvalid>();

        repositoryMock.Verify(
            r => r.GetUserByEmail(
                It.IsAny<string>()),
            Times.Once);
    }
}
