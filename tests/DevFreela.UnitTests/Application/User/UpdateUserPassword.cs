
using DevFreela.Application.UseCases.User.UpdatePassword;
using DevFreela.Domain.Domain.Exceptions;
using DevFreela.UnitTests.Application.Common;
using Moq;

namespace DevFreela.UnitTests.Application.User;
[Collection(nameof(UserApplicationTestFixture))]
public class UpdateUserPassword
{
    private readonly UserApplicationTestFixture _fixture;

    public UpdateUserPassword(UserApplicationTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(UpdateUserPasswordApplication))]
    [Trait("Application", "Update - User")]
    public async Task UpdateUserPasswordApplication()
    {
        var repositoryMock = _fixture.GetUserRepositoryMock();
        var authServiceMock = _fixture.GetAuthRepositoryMock();
        var oldPassword = _fixture.GetValidPassword();
        var user = _fixture.GetValidUser(password: oldPassword);

        var newPassword = _fixture.GetValidPassword();
        repositoryMock.Setup(r =>
                r.GetById(user.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        var useCase = new UserUseCase.UpdatePassword.UpdatePassword(
            repositoryMock.Object,
            authServiceMock.Object);

        var input = new UpdatePasswordInput(
            user.Id,
            oldPassword,
           newPassword
        );
        await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(
            r => r.GetById(
                user.Id,
                It.IsAny<CancellationToken>()),
            Times.Once);

        user.Password.Should().NotBe(_fixture.ComputeSha256Hash(password: oldPassword));
        user.Password.Should().Be(_fixture.ComputeSha256Hash(password: newPassword));
        user.OldPassword.Should().Be(_fixture.ComputeSha256Hash(password: oldPassword));


    }

    [Fact(DisplayName = nameof(ThrowWhenCurrentPasswordISNotMatch))]
    [Trait("Application", "Update - User")]
    public async Task ThrowWhenCurrentPasswordISNotMatch()
    {
        var repositoryMock = _fixture.GetUserRepositoryMock();
        var authServiceMock = _fixture.GetAuthRepositoryMock();

        var user = _fixture.GetValidUser();

        repositoryMock.Setup(r =>
                r.GetById(user.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        var useCase = new UserUseCase.UpdatePassword.UpdatePassword(
            repositoryMock.Object
            , authServiceMock.Object);

        var input = new UpdatePasswordInput(
            user.Id,
            oldPassword: "12345678",
            newPassword: "123456789"
        );
        var action = async () =>
            await useCase.Handle(input, CancellationToken.None);

        await action.Should().ThrowAsync<EntityValidationExceptions>();


    }

    [Fact(DisplayName = nameof(ThrowWhenNewPasswordIsSameTheCurrentPassword))]
    [Trait("Application", "Update - User")]
    public async Task ThrowWhenNewPasswordIsSameTheCurrentPassword()
    {
        var repositoryMock = _fixture.GetUserRepositoryMock();
        var authServiceMock = _fixture.GetAuthRepositoryMock();

        var user = _fixture.GetValidUser();

        repositoryMock.Setup(r =>
                r.GetById(user.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        var useCase = new UserUseCase.UpdatePassword.UpdatePassword(
            repositoryMock.Object,
            authServiceMock.Object);

        var input = new UpdatePasswordInput(
            user.Id,
            oldPassword: user.Password,
            newPassword: user.Password
        );
        var action = async () =>
            await useCase.Handle(input, CancellationToken.None);

        await action.Should().ThrowAsync<EntityValidationExceptions>();


    }

}
