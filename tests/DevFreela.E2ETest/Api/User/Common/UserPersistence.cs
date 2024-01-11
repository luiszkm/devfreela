

using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.E2ETest.Api.User.Common;
public class UserPersistence
{

    private readonly DevFreelaDbContext _dbContext;

    public UserPersistence(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<DomainEntity.User?> GetById(Guid id)
        => await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);

    public async Task InsertList(List<DomainEntity.User> users)
    {
        await _dbContext.Users.AddRangeAsync(users);
        await _dbContext.SaveChangesAsync();
    }
}
