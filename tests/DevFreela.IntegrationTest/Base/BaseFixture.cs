

using System.Security.Cryptography;
using System.Text;
using Bogus;
using DevFreela.Domain.Domain.Enums;
using DevFreela.Infrastructure.Models;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.IntegrationTest.Base;
public class BaseFixture
{
    public Faker Faker { get; set; }

    public BaseFixture()
        => Faker = new Faker("pt_BR");
    public Decimal GetRandomDecimal()
        => new Random().Next(1, 10000);
    public bool GetRandomBoolean()
        => new Random().NextDouble() < 0.5;

    public Guid GetRandomGuid()
        => Guid.NewGuid();

    public string GetValidEmail()
        => Faker.Internet.Email().ToLower();

    public string GetValidPassword()
        => Faker.Internet.Password(8, false, "", "@1Ab_");

    public string GetValidName()
        => Faker.Name.FullName();

    public DateTime GetValidBirthDate()
    => Faker.Date.Past(18);

    public string GetValidDescription()
        => Faker.Lorem.Paragraph();

    public string GetPasswordHash(string password)
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

    public FreelancersInterested GetValidFreelancersInterested(Guid projectId,
        Guid userId)
        => new(projectId, userId);

    public DomainEntity.User GetValidUser(
        UserRole role = UserRole.Client,
        string? email = null,
        string? password = null)
        => new(
            GetValidName(),
            email ?? GetValidEmail(),
            password != null ? GetPasswordHash(password) : GetValidPassword(),
            GetValidBirthDate(),
            role);

    public List<DomainEntity.Skill> GetValidSkillList()
    {
        var skills = new List<DomainEntity.Skill>();
        for (int i = 0; i < 5; i++)
        {
            skills.Add(new DomainEntity.Skill(
                Faker.Company.Random.Words()));
        }

        return skills;
    }

    public DomainEntity.Project GetValidProject(Guid? idClient = null)
        => new(
            GetValidName(),
            GetValidDescription(),
            1000,
            idClient ?? GetRandomGuid());


    public List<DomainEntity.Project> GetExampleProjectList(int length = 10)
        => Enumerable.Range(1, length)
            .Select(_ => GetValidProject())
            .ToList();

    public void ClearDatabase()
    {
        using var dbContext = CreateDbContext();
        dbContext.Database.EnsureDeleted();
    }

    public DevFreelaDbContext CreateDbContext(bool preserverData = false)
    {
        var context = new DevFreelaDbContext(
            new DbContextOptionsBuilder<DevFreelaDbContext>()
                .UseInMemoryDatabase("DevFreela-In-Memory")
                .Options);

        if (!preserverData)
            context.Database.EnsureCreated();

        return context;
    }
}
