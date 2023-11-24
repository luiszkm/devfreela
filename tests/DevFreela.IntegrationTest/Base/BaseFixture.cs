

using Bogus;
using DevFreela.Domain.Domain.Enums;
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

    public DomainEntity.User GetValidUser()
        => new(
            GetValidName(),
            GetValidName(),
            GetValidPassword(),
            GetValidBirthDate(),
            UserRole.Client);

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
