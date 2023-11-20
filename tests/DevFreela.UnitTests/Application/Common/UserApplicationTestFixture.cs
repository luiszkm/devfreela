
using System.Security.Cryptography;
using DevFreela.Domain.Domain.Authorization;
using DevFreela.Domain.Domain.Enums;
using DevFreela.Domain.Domain.Repository;
using DevFreela.UnitTests.Common;
using Moq;
using System.Text;

namespace DevFreela.UnitTests.Application.Common;

[CollectionDefinition(nameof(UserApplicationTestFixture))]

public class UserApplicationTestFixtureCollection : ICollectionFixture<UserApplicationTestFixture> { }
public class UserApplicationTestFixture : BaseFixture
{
    public string GetValidName()
        => Faker.Person.FullName;

    public string GetValidEmail()
        => Faker.Person.Email;

    public string GetValidPassword()
        => Faker.Internet.Password();

    public string GetInvalidPassword()
        => Faker.Internet.Password(7);
    public DateTime GetValidBirthDate()
        => Faker.Person.DateOfBirth;

    private string GetPasswordHash(string password)
    {
        var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        var builder = new StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
            builder.Append(hash[i].ToString("X2"));
        }

        return builder.ToString();
    }

    public DomainEntity.User GetValidUser(
        string? email = null,
        string? password = null)
        => new(
            GetValidName(),
            email ?? GetValidEmail(),
            password != null ? GetPasswordHash(password) : GetValidPassword(),
            GetValidBirthDate(),
            UserRole.Client
        );

    public UserUseCase.CreateUser.CreateUserInput GetValidInput(
        UserRole role = UserRole.Client)
        => new(
            GetValidName(),
           GetValidEmail(),
            GetValidBirthDate(),
            GetValidPassword(),
            role);

    public UserUseCase.UpdateUser.UpdateUserInput GetValidUpdateInput(Guid userId)
        => new(
            userId,
            GetValidName(),
            GetValidEmail(),
            GetValidBirthDate());


    public Mock<IUserRepository> GetUserRepositoryMock()
        => new();

    public Mock<IUserRepository> GetUserRepositoryMockWithUser(string email, string? password = null)
    {
        var mock = GetUserRepositoryMock();
        var passwordHash = GetPasswordHash(password ?? GetValidPassword());
        var user = GetValidUser(email, passwordHash);
        mock.Setup(u => u.GetById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Guid id, CancellationToken cancellationToken) => user);
        return mock;
    }




    public Mock<IAuthorization> GetAuthRepositoryMock()
    {
        var mock = new Mock<IAuthorization>();

        mock.Setup(a => a.ComputeSha256Hash(It.IsAny<string>()))
            .Returns((string password) => ComputeSha256Hash(password));

        return mock;

    }

    public string ComputeSha256Hash(string password)
    {
        System.Diagnostics.Debug.WriteLine($"Computing SHA-256 hash for password: {password}");
        var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        var builder = new StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
            builder.Append(hash[i].ToString("X2"));
        }

        return builder.ToString();
    }

}
