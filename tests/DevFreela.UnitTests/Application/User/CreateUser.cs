

using DevFreela.Application.Exceptions;
using DevFreela.Domain.Domain.Exceptions;
using DevFreela.UnitTests.Application.Common;
using Moq;

namespace DevFreela.UnitTests.Application.User;

[Collection(nameof(UserApplicationTestFixture))]
public class CreateUser
{
    private readonly UserApplicationTestFixture _fixture;

    public CreateUser(UserApplicationTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(CreateUserApplication))]
    [Trait("Application", "Create - User")]
    public async Task CreateUserApplication()
    {
        var repositoryMock = _fixture.GetUserRepositoryMock();
        var authRepositoryMock = _fixture.GetAuthRepositoryMock();
        var useCase = new UserUseCase.CreateUser.CreateUser(
            repositoryMock.Object,
            authRepositoryMock.Object);

        var input = _fixture.GetValidInput();
        var output = await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(
            r => r.Create(
                It.IsAny<DomainEntity.User>(),
                It.IsAny<CancellationToken>()),
            Times.Once);

        authRepositoryMock.Verify(
            a => a.ComputeSha256Hash(
                It.IsAny<string>()), Times.Once);

        output.Should().NotBeNull();
        output.Name.Should().Be(input.Name);
        output.Email.Should().Be(input.Email);
        output.Id.Should().NotBeEmpty();
        output.BirthDate.Should().Be(input.BirthDate);
        output.Skills.Should().HaveCount(0);




    }

    [Fact(DisplayName = nameof(ThrowWhenUserExists))]
    [Trait("Application", "Create - User")]
    public async Task ThrowWhenUserExists()
    {
        var email = _fixture.GetValidEmail();
        var repositoryMock = _fixture.GetUserRepositoryMockWithUser(email);
        repositoryMock.Setup(r => r.GetUserByEmail(email))
            .ReturnsAsync(_fixture.GetValidUser(email));

        var useCase = new UserUseCase.CreateUser.CreateUser(
            repositoryMock.Object,
            _fixture.GetAuthRepositoryMock().Object);

        var input = _fixture.GetValidInput();
        input.Email = email;

        var action = async () => await useCase.Handle(input, CancellationToken.None);
        await action.Should().ThrowAsync<AggregateExistsExceptions>();

        repositoryMock.Verify(
            r => r.Create(
                It.IsAny<DomainEntity.User>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Theory(DisplayName = nameof(ThrowWhenInvalidName))]
    [Trait("Application", "Create - User")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    [InlineData("aa")]
    public async Task ThrowWhenInvalidName(string name)
    {
        var repositoryMock = _fixture.GetUserRepositoryMock();
        var useCase = new UserUseCase.CreateUser.CreateUser(
            repositoryMock.Object,
            _fixture.GetAuthRepositoryMock().Object);
        var input = _fixture.GetValidInput();
        input.Name = name;
        var action = async () => await useCase.Handle(input, CancellationToken.None);
        await action.Should().ThrowAsync<EntityValidationExceptions>();
        repositoryMock.Verify(
            r => r.Create(
                It.IsAny<DomainEntity.User>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }
    [Theory(DisplayName = nameof(ThrowWhenInvalidEmail))]
    [Trait("Application", "Create - User")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    [InlineData("aa")]
    public async Task ThrowWhenInvalidEmail(string email)
    {
        var repositoryMock = _fixture.GetUserRepositoryMock();
        var useCase = new UserUseCase.CreateUser.CreateUser(
            repositoryMock.Object,
            _fixture.GetAuthRepositoryMock().Object);
        var input = _fixture.GetValidInput();
        input.Email = email;
        var action = async () => await useCase.Handle(input, CancellationToken.None);
        await action.Should().ThrowAsync<EntityValidationExceptions>();
        repositoryMock.Verify(
            r => r.Create(
                It.IsAny<DomainEntity.User>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Theory(DisplayName = nameof(ThrowWhenInvalidPassword))]
    [Trait("Application", "Create - User")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    [InlineData("1234567")]
    public async Task ThrowWhenInvalidPassword(string password)
    {
        var repositoryMock = _fixture.GetUserRepositoryMock();
        var useCase = new UserUseCase.CreateUser.CreateUser(
            repositoryMock.Object,
            _fixture.GetAuthRepositoryMock().Object);
        var input = _fixture.GetValidInput();
        input.Password = password;
        var action = async () => await useCase.Handle(input, CancellationToken.None);
        await action.Should().ThrowAsync<EntityValidationExceptions>();
        repositoryMock.Verify(
            r => r.Create(
                It.IsAny<DomainEntity.User>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }
}
