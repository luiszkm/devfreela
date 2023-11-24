using DevFreela.Application.Exceptions;
using DevFreela.Domain.Domain.Entities;
using DevFreela.Domain.Domain.Entities.Models;
using DevFreela.Domain.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace DevFreela.Infrastructure.Persistence.Repository;
public class UserRepository : IUserRepository
{
    private readonly DevFreelaDbContext _dbContext;

    public UserRepository(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    private DbSet<User> _user => _dbContext.Set<User>();
    private DbSet<UserSkills> _userSkills => _dbContext.Set<UserSkills>();


    public async Task Create(User aggregate, CancellationToken cancellationToken)
    {
        await _user.AddAsync(aggregate, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }


    public async Task<User> GetById(Guid id, CancellationToken cancellationToken)
    {
        var user = await _user.AsNoTracking()
            .SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
        if (user == null)
            throw new NotFoundException();
        var userSkills = await _userSkills.AsNoTracking()
            .Where(u => u.IdUser == id)
            .ToListAsync(cancellationToken);



        var skillsList = new List<Skill>();
        foreach (var item in userSkills)
        {
            var skill = await _dbContext.Skills.AsNoTracking()
                .SingleOrDefaultAsync(s => s.Id == item.IdSkill, cancellationToken);
            skillsList.Add(skill);
        }
        user.AddSkills(skillsList);

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
            throw new NotFoundException();
        _user.Remove(user);
        _userSkills.RemoveRange(_userSkills.Where(u => u.IdUser == aggregate.Id));
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        var user = await _user.SingleOrDefaultAsync(u => u.Email == email);
        return user;
    }

    public async Task<User?> AddSkill(Guid userId, List<Skill> skill)
    {
        var user = await _user.AsNoTracking()
            .SingleOrDefaultAsync(u => u.Id == userId);
        if (user == null)
            throw new NotFoundException();

        foreach (var item in skill)
        {
            var userSkill = new UserSkills(userId, item.Id);
            await _userSkills.AddAsync(userSkill);
        }

        await _dbContext.SaveChangesAsync();
        user.AddSkills(skill);
        return user;
    }
}
