using DevFreela.Application.Exceptions;
using DevFreela.Domain.Domain.Entities.Models;
using DevFreela.Infrastructure.Persistence.Repository;

namespace DevFreela.IntegrationTest.Infra.Repositories.User;


[Collection(nameof(UserRepositoryTestFixture))]
public class UserRepositoryTest : IDisposable
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

        dbContext.Users.Should().Contain(user);

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

    [Fact(DisplayName = nameof(GetUserWithSkill))]
    [Trait("Infra", "UserRepository - Repository")]
    public async Task GetUserWithSkill()
    {
        var dbContext = _fixture.CreateDbContext();
        var userExample = _fixture.GetValidUser();
        var skills = _fixture.GetValidSkillList();

        await dbContext.Users.AddRangeAsync(userExample);
        await dbContext.Skills.AddRangeAsync(skills);

        foreach (var skill in skills)
        {
            await dbContext.UserSkills.AddAsync(new UserSkills(userExample.Id, skill.Id));

        }

        await dbContext.SaveChangesAsync(CancellationToken.None);

        var userRepository = new UserRepository(_fixture.CreateDbContext(true));

        var userFound = await userRepository.GetById(userExample.Id, CancellationToken.None);

        dbContext.UserSkills.Should().NotBeEmpty();
        dbContext.UserSkills.Should().HaveCount(skills.Count);

        userFound.Should().NotBeNull();

        userFound.Skills.Should().NotBeEmpty();
        userFound.Skills.Should().HaveCount(skills.Count);

        userFound.Name.Should().Be(userExample.Name);
        userFound.Email.Should().Be(userExample.Email);
        userFound.BirthDate.Should().Be(userExample.BirthDate);
        userFound.CreatedAt.Should().Be(userExample.CreatedAt);
        userFound.Active.Should().Be(userExample.Active);

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

        userRepository!.Delete(listUsers[3], CancellationToken.None);

        dbContext.Users.Should().NotContain(listUsers[3]);

    }

    [Fact(DisplayName = nameof(AddUserSkill))]
    [Trait("Infra", "UserRepository - Repository")]
    public async Task AddUserSkill()
    {
        var dbContext = _fixture.CreateDbContext();
        var userExample = _fixture.GetValidUser();
        await dbContext.Users.AddAsync(userExample);
        await dbContext.SaveChangesAsync(CancellationToken.None);
        var skills = _fixture.GetValidSkillList();

        var userRepository = new UserRepository(dbContext);

        var user = await userRepository.AddSkill(userExample.Id, skills);

        dbContext.UserSkills.Should().NotBeEmpty();
        dbContext.UserSkills.Should().HaveCount(skills.Count);

        user!.Skills.Should().NotBeEmpty();
        user!.Skills.Should().HaveCount(skills.Count);
    }
    public void Dispose()
    {
        _fixture.ClearDatabase();
    }
}
