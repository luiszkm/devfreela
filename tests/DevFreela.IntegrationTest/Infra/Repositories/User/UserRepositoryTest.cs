using DevFreela.Application.Exceptions;
using DevFreela.Infrastructure.Persistence.Repository;

namespace DevFreela.IntegrationTest.Infra.Repositories.User;


[Collection(nameof(UserRepositoryTestFixture))]
public class UserRepositoryTest
{
    private readonly UserRepositoryTestFixture _fixture;

    public UserRepositoryTest(UserRepositoryTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(InsertUser))]
    [Trait("Infra", "UserRepository - Repository")]

    public async Task InsertUser()
    {
        var dbContext = _fixture.CreateDbContext();
        var user = _fixture.GetValidUser();
        var userRepository = new UserRepository(dbContext);

        await userRepository.Create(user, CancellationToken.None);
        await dbContext.SaveChangesAsync(CancellationToken.None);

        var userInserted = await userRepository.GetById(user.Id, CancellationToken.None);

        dbContext.Users.Should().Contain(userInserted);

        userInserted.Should().NotBeNull();
        userInserted.Name.Should().Be(user.Name);
        userInserted.Email.Should().Be(user.Email);
        userInserted.BirthDate.Should().Be(user.BirthDate);
        userInserted.CreatedAt.Should().Be(user.CreatedAt);
        userInserted.Active.Should().Be(user.Active);
    }

    [Fact(DisplayName = nameof(ThrowWhenUserExists))]
    [Trait("Infra", "UserRepository - Repository")]

    public async Task ThrowWhenUserExists()
    {
        var dbContext = _fixture.CreateDbContext();
        var user = _fixture.GetValidUser();
        var userRepository = new UserRepository(dbContext);

        await dbContext.Users.AddAsync(user, CancellationToken.None);
        await dbContext.SaveChangesAsync(CancellationToken.None);

        var action = async () =>
            await userRepository.Create(user, CancellationToken.None);
        action.Should().ThrowAsync<AggregateExistsExceptions>();

    }

    [Fact(DisplayName = nameof(GetUser))]
    [Trait("Infra", "UserRepository - Repository")]
    public async Task GetUser()
    {
        var dbContext = _fixture.CreateDbContext();
        var listUsers = _fixture.GetExampleUsersList();
        var user = _fixture.GetValidUser();
        await dbContext.Users.AddRangeAsync(listUsers);
        var userRepository = new UserRepository(dbContext);
        await userRepository.Create(user, CancellationToken.None);
        await dbContext.SaveChangesAsync(CancellationToken.None);

        var userFound = await userRepository.GetById(user.Id, CancellationToken.None);

        userFound.Should().NotBeNull();
        userFound.Name.Should().Be(user.Name);
        userFound.Email.Should().Be(user.Email);
        userFound.BirthDate.Should().Be(user.BirthDate);
        userFound.CreatedAt.Should().Be(user.CreatedAt);
        userFound.Active.Should().Be(user.Active);

    }


    [Fact(DisplayName = nameof(GetUserByEmail))]
    [Trait("Infra", "UserRepository - Repository")]
    public async Task GetUserByEmail()
    {
        var dbContext = _fixture.CreateDbContext();
        var listUsers = _fixture.GetExampleUsersList();
        var user = _fixture.GetValidUser();
        await dbContext.Users.AddRangeAsync(listUsers);
        var userRepository = new UserRepository(dbContext);
        await userRepository.Create(user, CancellationToken.None);
        await dbContext.SaveChangesAsync(CancellationToken.None);

        var userFound = await userRepository.GetUserByEmail(user.Email);

        userFound.Should().NotBeNull();
        userFound.Name.Should().Be(user.Name);
        userFound.Email.Should().Be(user.Email);
        userFound.BirthDate.Should().Be(user.BirthDate);
        userFound.CreatedAt.Should().Be(user.CreatedAt);
        userFound.Active.Should().Be(user.Active);

    }
    [Fact(DisplayName = nameof(ThrowWhenNotFoundUser))]
    [Trait("Infra", "UserRepository - Repository")]
    public async Task ThrowWhenNotFoundUser()
    {
        var dbContext = _fixture.CreateDbContext();
        var listUsers = _fixture.GetExampleUsersList();
        var user = _fixture.GetValidUser();
        await dbContext.Users.AddRangeAsync(listUsers);
        var userRepository = new UserRepository(dbContext);
        await dbContext.SaveChangesAsync(CancellationToken.None);

        var action = async () =>
            await userRepository.GetById(user.Id, CancellationToken.None);
        action.Should().ThrowAsync<NotFoundException>();

    }


    [Fact(DisplayName = nameof(UpdateUser))]
    [Trait("Infra", "UserRepository - Repository")]
    public async Task UpdateUser()
    {
        var dbContext = _fixture.CreateDbContext();
        var listUsers = _fixture.GetExampleUsersList();
        var userUpdated = _fixture.GetValidUser();

        await dbContext.Users.AddRangeAsync(listUsers);
        await dbContext.SaveChangesAsync(CancellationToken.None);

        var userRepository = new UserRepository(dbContext);

        listUsers[3].Update(
            userUpdated.Name,
            userUpdated.Email,
            userUpdated.BirthDate);

        await userRepository.Update(listUsers[3], CancellationToken.None);
        var userToUpdate = await userRepository.GetById(listUsers[3].Id, CancellationToken.None);

        userToUpdate.Should().NotBeNull();
        userToUpdate.Name.Should().Be(userUpdated.Name);
        userToUpdate.Email.Should().Be(userUpdated.Email);
        userToUpdate.BirthDate.Should().Be(userUpdated.BirthDate);
        userToUpdate.CreatedAt.Should().Be(listUsers[3].CreatedAt);
        userToUpdate.Active.Should().Be(listUsers[3].Active);

    }

    [Fact(DisplayName = nameof(UpdateUserPassword))]
    [Trait("Infra", "UserRepository - Repository")]
    public async Task UpdateUserPassword()
    {
        var dbContext = _fixture.CreateDbContext();
        var listUsers = _fixture.GetExampleUsersList();
        var userUpdated = _fixture.GetValidUser();
        var newPassword = "DevFreela@123";
        await dbContext.Users.AddRangeAsync(listUsers);
        await dbContext.SaveChangesAsync(CancellationToken.None);

        var userRepository = new UserRepository(dbContext);

        listUsers[3].UpdatePassword(
            userUpdated.Password,
            newPassword);

        await userRepository.Update(listUsers[3], CancellationToken.None);
        var userToUpdate = await userRepository.GetById(listUsers[3].Id, CancellationToken.None);

        userToUpdate.Should().NotBeNull();
        userToUpdate.Name.Should().Be(listUsers[3].Name);
        userToUpdate.Email.Should().Be(listUsers[3].Email);
        userToUpdate.BirthDate.Should().Be(listUsers[3].BirthDate);
        userToUpdate.CreatedAt.Should().Be(listUsers[3].CreatedAt);
        userToUpdate.Active.Should().Be(listUsers[3].Active);
        userToUpdate.Password.Should().Be(newPassword);


    }

    [Fact(DisplayName = nameof(DeleteUser))]
    [Trait("Infra", "UserRepository - Repository")]
    public async Task DeleteUser()
    {
        var dbContext = _fixture.CreateDbContext();
        var listUsers = _fixture.GetExampleUsersList();

        await dbContext.Users.AddRangeAsync(listUsers);
        await dbContext.SaveChangesAsync(CancellationToken.None);

        var userRepository = new UserRepository(dbContext);

        userRepository.Delete(listUsers[3], CancellationToken.None);

        dbContext.Users.Should().NotContain(listUsers[3]);

    }
}
