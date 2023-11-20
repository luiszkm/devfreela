

using System.Security.Cryptography;
using Bogus;
using DevFreela.Domain.Domain.Enums;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text;


namespace DevFreela.E2ETest.Base;
public class BaseFixture
{
    public BaseFixture()
    {
        Faker = new Faker("pt_BR");
        WebAppFactory = new CustomWebApplicationFactory<Program>();
        HttpClient = WebAppFactory.CreateClient();
        ApiClient = new ApiClient(HttpClient);
        var config = WebAppFactory.Services
            .GetService(typeof(IConfiguration));
        ArgumentNullException.ThrowIfNull(config);
        _dbConnectionString = ((IConfiguration)config)
            .GetConnectionString("devfreela_e2e");
    }
    public ApiClient ApiClient { get; set; }
    public HttpClient HttpClient { get; set; }
    public CustomWebApplicationFactory<Program> WebAppFactory { get; set; }

    private readonly string _dbConnectionString;

    public Faker Faker { get; set; }

    public string GetInvalidEmail()
        => "aco";
    public string GetInvalidName()
        => "a";
    public string GetValidEmail()
        => Faker.Internet.Email().ToLower();

    public string GetValidPassword()
        => Faker.Internet.Password(8, false, "", "@1Ab_");

    public string GetValidName()
        => Faker.Name.FullName();

    public DateTime GetValidBirthDate()
        => Faker.Date.Past(18);


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
            UserRole role = UserRole.Client,
            string? email = null,
            string? password = null)
            => new(
                GetValidName(),
                email ?? GetValidEmail(),
                password != null ? GetPasswordHash(password) : GetValidPassword(),
                GetValidBirthDate(),
                role);

    public async Task<(DomainEntity.User user, string password)> GetUserInDataBase()
    {
        var password = GetValidPassword();
        var dbContext = CreateApiDbContextInMemory();
        var user = GetValidUser(password: password);
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();

        return (user, password);
    }

    public DevFreelaDbContext CreateApiDbContext()
    {
        var context = new DevFreelaDbContext(
            new DbContextOptionsBuilder<DevFreelaDbContext>()
                .UseMySql(
                    _dbConnectionString,
                    ServerVersion.AutoDetect(_dbConnectionString))
                .Options
        );
        return context;
    }

    public DevFreelaDbContext CreateApiDbContextInMemory(bool preserverData = false)
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
