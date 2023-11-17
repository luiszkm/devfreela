using DevFreela.Domain.Domain.Exceptions;

namespace DevFreela.UnitTests.Domain.Entities.User;

[Collection(nameof(UserTestFixture))]
public class UserTest
{

    private readonly UserTestFixture _fixture;

    public UserTest(UserTestFixture fixture)
    => _fixture = fixture;


    [Fact(DisplayName = nameof(InstantiateUser))]
    [Trait("Domain", "User - Instantiation")]
    public void InstantiateUser()
    {
        var fixture = _fixture.CreateValidUser();
        var user = new DomainEntity.User(
                       fixture.Name,
                       fixture.Email,
                       fixture.Password,
                       fixture.BirthDate,
                       fixture.Role);

        user.Should().NotBeNull();
        user.Id.Should().NotBeEmpty();
        user.Name.Should().Be(fixture.Name);
        user.Email.Should().Be(fixture.Email);
        user.Password.Should().Be(fixture.Password);
        user.BirthDate.Should().Be(fixture.BirthDate);
        user.Role.Should().Be(fixture.Role);
        user.Active.Should().BeTrue();
        user.CreatedAt.Should().BeBefore(DateTime.Now);
        (user.UpdatedAt > user.CreatedAt).Should().BeTrue();
        user.Skills.Should().BeEmpty();
        user.FreelanceProjects.Should().BeEmpty();
        user.OwnedProjects.Should().BeEmpty();
        user.Comments.Should().BeEmpty();


    }

    [Fact(DisplayName = nameof(UpdateUser))]
    [Trait("Domain", "User - Instantiation")]
    public void UpdateUser()
    {
        var user = _fixture.CreateValidUser();
        var nameUpdated = _fixture.GetValidName();
        var emailUpdated = _fixture.GetValidEmail();
        var birthDateUpdated = _fixture.GetValidBirthDate();
        user.Update(
            name: nameUpdated,
            email: emailUpdated,
            birthDate: birthDateUpdated);

        user.Name.Should().Be(nameUpdated);
        user.Email.Should().Be(emailUpdated);
        user.BirthDate.Should().Be(birthDateUpdated);


        user.Should().NotBeNull();
        user.Id.Should().NotBeEmpty();
        user.Active.Should().BeTrue();
        user.CreatedAt.Should().BeBefore(DateTime.Now);
        (user.UpdatedAt > user.CreatedAt).Should().BeTrue();
        user.Skills.Should().BeEmpty();
        user.FreelanceProjects.Should().BeEmpty();
        user.OwnedProjects.Should().BeEmpty();
        user.Comments.Should().BeEmpty();

    }


    [Fact(DisplayName = nameof(UpdateUserOnlyEmail))]
    [Trait("Domain", "User - Instantiation")]
    public void UpdateUserOnlyEmail()
    {
        var user = _fixture.CreateValidUser();
        var emailUpdated = _fixture.GetValidEmail();
        user.Update(email: emailUpdated);

        user.Name.Should().Be(user.Name);
        user.Email.Should().Be(emailUpdated);
        user.BirthDate.Should().Be(user.BirthDate);

        user.Should().NotBeNull();
        user.Id.Should().NotBeEmpty();
        user.Active.Should().BeTrue();
        user.CreatedAt.Should().BeBefore(DateTime.Now);
        (user.UpdatedAt > user.CreatedAt).Should().BeTrue();
        user.Skills.Should().BeEmpty();
        user.FreelanceProjects.Should().BeEmpty();
        user.OwnedProjects.Should().BeEmpty();
        user.Comments.Should().BeEmpty();

    }
    [Fact(DisplayName = nameof(UpdateUserOnlyBDay))]
    [Trait("Domain", "User - Instantiation")]

    public void UpdateUserOnlyBDay()
    {
        var user = _fixture.CreateValidUser();
        var birthDateUpdated = _fixture.GetValidBirthDate();
        user.Update(
            birthDate: birthDateUpdated);

        user.Name.Should().Be(user.Name);
        user.Email.Should().Be(user.Email);
        user.BirthDate.Should().Be(birthDateUpdated);

        user.Should().NotBeNull();
        user.Id.Should().NotBeEmpty();
        user.Active.Should().BeTrue();
        user.CreatedAt.Should().BeBefore(DateTime.Now);
        (user.UpdatedAt > user.CreatedAt).Should().BeTrue();
        user.Skills.Should().BeEmpty();
        user.FreelanceProjects.Should().BeEmpty();
        user.OwnedProjects.Should().BeEmpty();
        user.Comments.Should().BeEmpty();

    }

    [Fact(DisplayName = nameof(UpdateUserOnlyName))]
    [Trait("Domain", "User - Instantiation")]

    public void UpdateUserOnlyName()
    {
        var user = _fixture.CreateValidUser();
        var nameUpdated = _fixture.GetValidName();

        user.Update(
            name: nameUpdated);

        user.Name.Should().Be(nameUpdated);
        user.Email.Should().Be(user.Email);
        user.BirthDate.Should().Be(user.BirthDate);

        user.Should().NotBeNull();
        user.Id.Should().NotBeEmpty();
        user.Active.Should().BeTrue();
        user.CreatedAt.Should().BeBefore(DateTime.Now);
        (user.UpdatedAt > user.CreatedAt).Should().BeTrue();
        user.Skills.Should().BeEmpty();
        user.FreelanceProjects.Should().BeEmpty();
        user.OwnedProjects.Should().BeEmpty();
        user.Comments.Should().BeEmpty();


    }



    [Theory(DisplayName = nameof(UpdatePassword))]
    [Trait("Domain", "User - Instantiation")]
    [InlineData("12345678")]
    [InlineData("Dev@1234")]

    public void UpdatePassword(string newPassword)
    {
        var password = _fixture.GetValidPassword();
        var fixture = _fixture.CreateValidUser();
        var user = new DomainEntity.User(
            fixture.Name,
            fixture.Email,
            password,
            fixture.BirthDate,
            fixture.Role);
        user.UpdatePassword(password, newPassword);
        user.Password.Should().Be(newPassword);
        user.Name.Should().Be(user.Name);
        user.Email.Should().Be(user.Email);
        user.BirthDate.Should().Be(user.BirthDate);
        user.Should().NotBeNull();
        user.Id.Should().NotBeEmpty();
        user.Active.Should().BeTrue();
        user.CreatedAt.Should().BeBefore(DateTime.Now);
        (user.UpdatedAt > user.CreatedAt).Should().BeTrue();
        user.Skills.Should().BeEmpty();
        user.FreelanceProjects.Should().BeEmpty();
        user.OwnedProjects.Should().BeEmpty();
        user.Comments.Should().BeEmpty();
    }

    [Theory(DisplayName = nameof(ThrowWhenNameIsInvalid))]
    [Trait("Domain", "User - Instantiation")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("a")]
    [InlineData("    ")]

    public void ThrowWhenNameIsInvalid(string? name)
    {
        var fixture = _fixture.CreateValidUser();

        var action = () => new DomainEntity.User(
            name,
            fixture.Email,
            fixture.Password,
            fixture.BirthDate,
            fixture.Role);

        action.Should().Throw<EntityValidationExceptions>();


    }
    [Theory(DisplayName = nameof(ThrowWhenEmailIsInvalid))]
    [Trait("Domain", "User - Instantiation")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("a")]
    [InlineData("aaaa")]
    [InlineData("    ")]

    public void ThrowWhenEmailIsInvalid(string? email)
    {
        var fixture = _fixture.CreateValidUser();

        var action = () => new DomainEntity.User(
            fixture.Name,
            email,
            fixture.Password,
            fixture.BirthDate,
            fixture.Role);

        action.Should().Throw<EntityValidationExceptions>();


    }

    [Theory(DisplayName = nameof(ThrowWhenPasswordIsInvalid))]
    [Trait("Domain", "User - Instantiation")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("a")]
    [InlineData("1234567")]
    [InlineData("    ")]

    public void ThrowWhenPasswordIsInvalid(string? password)
    {
        var fixture = _fixture.CreateValidUser();

        var action = () => new DomainEntity.User(
            fixture.Name,
            fixture.Email,
            password,
            fixture.BirthDate,
            fixture.Role);

        action.Should().Throw<EntityValidationExceptions>();
    }


    [Theory(DisplayName = nameof(ThrowWhenPasswordIsInvalidForUpdate))]
    [Trait("Domain", "User - Instantiation")]
    [InlineData("")]
    [InlineData("a")]
    [InlineData("1234567")]
    [InlineData("    ")]

    public void ThrowWhenPasswordIsInvalidForUpdate(string password)
    {
        var user = _fixture.CreateValidUser();

        var action = () => user.UpdatePassword(user.Password, password);

        action.Should().Throw<EntityValidationExceptions>();

    }

    [Theory(DisplayName = nameof(ThrowWhenNameIsInvalidForUpdate))]
    [Trait("Domain", "User - Instantiation")]
    [InlineData("")]
    [InlineData("a")]
    [InlineData("    ")]

    public void ThrowWhenNameIsInvalidForUpdate(string name)
    {
        var user = _fixture.CreateValidUser();

        var action = () => user.Update(name: name);

        action.Should().Throw<EntityValidationExceptions>();


    }
    [Theory(DisplayName = nameof(ThrowWhenEmailIsInvalidForUpdate))]
    [Trait("Domain", "User - Instantiation")]
    [InlineData("")]
    [InlineData("a")]
    [InlineData("aaaa")]
    [InlineData("    ")]

    public void ThrowWhenEmailIsInvalidForUpdate(string email)
    {
        var user = _fixture.CreateValidUser();

        var action = () => user.Update(email: email);

        action.Should().Throw<EntityValidationExceptions>();

    }




}
