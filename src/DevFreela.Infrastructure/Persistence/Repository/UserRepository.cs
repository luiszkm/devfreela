using DevFreela.Application.Exceptions;
using DevFreela.Domain.Domain.Entities;
using DevFreela.Domain.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repository;
public class UserRepository : IUserRepository
{
    private readonly DevFreelaDbContext _dbContext;

    public UserRepository(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    private DbSet<User> _user => _dbContext.Set<User>();


    public async Task Create(User aggregate, CancellationToken cancellationToken)
    {
        await _user.AddAsync(aggregate, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }


    public async Task<User> GetById(Guid id, CancellationToken cancellationToken)
    {
        var user = await _user.SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
        if (user == null)
            throw new NotFoundException();
        return user;
    }



    public async Task Update(User aggregate, CancellationToken cancellationToken)
    {
        var user = await _user.SingleOrDefaultAsync(u => u.Id == aggregate.Id, cancellationToken);
        if (user == null)
            throw new NotFoundException();

        user.Update(aggregate.Name, aggregate.Email, aggregate.BirthDate);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(User aggregate, CancellationToken cancellationToken)
    {
        var user = await _user.SingleOrDefaultAsync(u => u.Id == aggregate.Id, cancellationToken);
        if (user == null)
        {
            return;
        }
        _user.Remove(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        var user = await _user.SingleOrDefaultAsync(u => u.Email == email);
        return user;
    }

    public async Task<User?> GetUserByEmailAndPassword(string email, string passwordHash)
    {
        var user = await _user.SingleOrDefaultAsync(u =>
            u.Email == email &&
            u.Password == passwordHash);

        if (user == null)
            throw new NotFoundException();
        return user;
    }
    public Task CreateSession(string email, string password)
    {
        var user = _user.SingleOrDefaultAsync(u => u.Email == email && u.Password == password);
        return Task.CompletedTask;
    }
}
