

using DevFreela.Application.Exceptions;
using DevFreela.Application.UseCases.User.UpdateUser;
using DevFreela.Domain.Domain.Exceptions;
using DevFreela.UnitTests.Application.Common;
using Moq;

namespace DevFreela.UnitTests.Application.User;
[Collection(nameof(UserApplicationTestFixture))]
public class UpdateUser
{
    private readonly UserApplicationTestFixture _fixture;

    public UpdateUser(UserApplicationTestFixture fixture)
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

        var useCase = new UserUseCase.UpdateUser.Updateuser(
            repositoryMock.Object);

        var input = _fixture.GetValidUpdateInput(user.Id);
        var output = await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(
            r => r.GetById(
                user.Id,
                It.IsAny<CancellationToken>()),
            Times.Once);

        output.Should().NotBeNull();
        output.Id.Should().Be(user.Id);
        output.Name.Should().Be(input.Name);
        output.Email.Should().Be(input.Email);
        output.BirthDate.Should().Be(input.BirthDate);

    }

    [Theory(DisplayName = nameof(UpdateUserApplicationOnlyName))]
    [Trait("Application", "Update - User")]
    [InlineData("Teste")]
    public async Task UpdateUserApplicationOnlyName(string name)
    {
        var repositoryMock = _fixture.GetUserRepositoryMock();
        var user = _fixture.GetValidUser();

        repositoryMock.Setup(r =>
            r.GetById(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);

        var useCase = new UserUseCase.UpdateUser.Updateuser(
            repositoryMock.Object);

        var input = new UpdateUserInput(user.Id, name: name);
        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Id.Should().Be(user.Id);
        output.Name.Should().Be(input.Name);
        output.Email.Should().Be(user.Email);
        output.BirthDate.Should().Be(user.BirthDate);

    }

    [Theory(DisplayName = nameof(UpdateUserApplicationOnlyName))]
    [Trait("Application", "Update - User")]
    [InlineData("Teste@email.com")]
    public async Task UpdateUserApplicationOnlyEmail(string email)
    {
        var repositoryMock = _fixture.GetUserRepositoryMock();
        var user = _fixture.GetValidUser();

        repositoryMock.Setup(r =>
            r.GetById(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);

        var useCase = new UserUseCase.UpdateUser.Updateuser(
            repositoryMock.Object);

        var input = new UpdateUserInput(user.Id, email: email);
        var output = await useCase.Handle(input, CancellationToken.None);



        output.Should().NotBeNull();
        output.Id.Should().Be(user.Id);
        output.Name.Should().Be(user.Name);
        output.Email.Should().Be(input.Email);
        output.BirthDate.Should().Be(user.BirthDate);

    }

    [Fact(DisplayName = nameof(UpdateUserApplicationOnlyName))]
    [Trait("Application", "Update - User")]

    public async Task UpdateUserApplicationOnlyBDay()
    {
        var repositoryMock = _fixture.GetUserRepositoryMock();
        var user = _fixture.GetValidUser();
        var BDay = _fixture.GetValidBirthDate();

        repositoryMock.Setup(r =>
            r.GetById(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);

        var useCase = new UserUseCase.UpdateUser.Updateuser(
            repositoryMock.Object);

        var input = new UpdateUserInput(user.Id, birthDate: BDay);
        var output = await useCase.Handle(input, CancellationToken.None);



        output.Should().NotBeNull();
        output.Id.Should().Be(user.Id);
        output.Name.Should().Be(user.Name);
        output.Email.Should().Be(user.Email);
        output.BirthDate.Should().Be(BDay);

    }

    [Fact(DisplayName = nameof(ThrowWhenUserNotFound))]
    [Trait("Application", "Update - User")]

    public async Task ThrowWhenUserNotFound()
    {
        var repositoryMock = _fixture.GetUserRepositoryMock();
        var user = _fixture.GetValidUser();
        var BDay = _fixture.GetValidBirthDate();
        var invalidUser = Guid.NewGuid();
        repositoryMock.Setup(r =>
            r.GetById(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);

        var useCase = new UserUseCase.UpdateUser.Updateuser(
            repositoryMock.Object);

        var input = new UpdateUserInput(invalidUser, birthDate: BDay);
        var action = async ()
            => await useCase.Handle(input, CancellationToken.None);

        action.Should().ThrowAsync<NotFoundException>();


    }

    [Theory(DisplayName = nameof(ThrowWhenInvalidName))]
    [Trait("Application", "Update - User")]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("12")]

    public async Task ThrowWhenInvalidName(string name)
    {
        var repositoryMock = _fixture.GetUserRepositoryMock();
        var user = _fixture.GetValidUser();
        repositoryMock.Setup(r =>
            r.GetById(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);

        var useCase = new UserUseCase.UpdateUser.Updateuser(
            repositoryMock.Object);

        var input = new UpdateUserInput(user.Id, name: name);
        var action = async ()
            => await useCase.Handle(input, CancellationToken.None);

        action.Should().ThrowAsync<EntityValidationExceptions>();

    }

    [Theory(DisplayName = nameof(ThrowWhenInvalidEmail))]
    [Trait("Application", "Update - User")]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("12")]
    [InlineData("te.com")]

    public async Task ThrowWhenInvalidEmail(string email)
    {
        var repositoryMock = _fixture.GetUserRepositoryMock();
        var user = _fixture.GetValidUser();
        repositoryMock.Setup(r =>
            r.GetById(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);

        var useCase = new UserUseCase.UpdateUser.Updateuser(
            repositoryMock.Object);

        var input = new UpdateUserInput(user.Id, email: email);
        var action = async ()
            => await useCase.Handle(input, CancellationToken.None);

        action.Should().ThrowAsync<EntityValidationExceptions>();

    }
}
