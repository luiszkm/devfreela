namespace DevFreela.UnitTests.Domain.Entities.User;

[Collection(nameof(UserTestFixture))]
public class UserTest
{

    private readonly UserTestFixture _fixture;

    public UserTest(UserTestFixture fixture)
    => _fixture = fixture;


    [Fact]
    public void InstantiateUser()
    {
        var birthDate = new DateTime(1997, 10, 10);
        var user = new DomainEntity.User(
            "Nome de teste",
            "test@email.com",
            "12345678",
            birthDate
        );

        user.Should().NotBeNull();
        user.Name.Should().Be("Nome de teste");
        user.Email.Should().Be("test@email.com");
        user.BirthDate.Should().Be(birthDate);
        user.CreatedAt.Should().BeBefore(DateTime.Now);
        user.Active.Should().BeTrue();
        (user.VerifyPassword("12345678")).Should().BeTrue();
    }

    [Fact]
    public void ThrowWhenPasswordIsWorng()
    {
        var user = _fixture.CreateValidUser();
        var invalidPassword = "123456789";
        (user.VerifyPassword(invalidPassword)).Should().BeFalse();

    }
}
