using DevFreela.Application.Exceptions;
using DevFreela.Domain.Domain.Authorization;
using DevFreela.Domain.Domain.Entities;
using DevFreela.Domain.Domain.Entities.Models;
using DevFreela.Domain.Domain.Exceptions;
using DevFreela.Domain.Domain.Repository;
using DevFreela.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace DevFreela.Infrastructure.Persistence.Repository;
public class UserRepository : IUserRepository
{
    private readonly DevFreelaDbContext _dbContext;
    private readonly IAuthorization _authorization;

    public UserRepository(DevFreelaDbContext dbContext,
        IAuthorization? authorization = null)
    {
        _dbContext = dbContext;
        _authorization = authorization;
    }
    private DbSet<User> _user => _dbContext.Set<User>();
    private DbSet<Skill> _skills => _dbContext.Set<Skill>();

    private DbSet<UserSkills> _userSkills => _dbContext.Set<UserSkills>();
    private DbSet<UserOwnedProjects> _userOwnedProjects => _dbContext.Set<UserOwnedProjects>();

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

        var skillsList = new List<Skill>();

        var userSkills = await _userSkills.AsNoTracking()
            .Where(u => u.IdUser == id)
            .ToListAsync(cancellationToken);


        foreach (var skill in userSkills)
        {
            var skills = await _skills.AsNoTracking()
                .Where(s => s.Id == skill.IdSkill)
                .ToListAsync(cancellationToken);
            skillsList.AddRange(skills);
        }
        user.AddSkills(skillsList);


        var userOwnedProjects = await _userOwnedProjects
            .AsNoTracking()
            .Where(u => u.IdUser == id)
            .ToListAsync(cancellationToken);

        foreach (var project in userOwnedProjects)
        {
            var projectFounded = await _dbContext.Projects
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == project.IdProject, cancellationToken);
            user.AddOwnedProject(projectFounded!);
        }

        var userFreelancerProjects = await _dbContext.Projects
            .AsNoTracking()
            .Where(p => p.IdFreelancer == id)
            .ToListAsync(cancellationToken);

        foreach (var project in userFreelancerProjects)
        {
            user.AddFreelanceProject(project);
        }


        return user;
    }

    public async Task Update(User aggregate, CancellationToken cancellationToken)
    {
        var user = await _user.SingleOrDefaultAsync(u => u.Id == aggregate.Id, cancellationToken);
        if (user == null)
            throw new NotFoundException();

        user.Update(aggregate.Name, aggregate.Email, aggregate.BirthDate);
        _user.Update(user);
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

        if (skill == null)
            throw new BadRequestException("Skill is null");

        if (skill != null && skill.Count > 0)
        {
            var userSkills = await _userSkills.AsNoTracking()
                .Where(u => u.IdUser == userId)
                .ToListAsync();
            _userSkills.RemoveRange(userSkills);
        }


        foreach (var item in skill)
        {
            var userSkill = new UserSkills(userId, item.Id);
            await _userSkills.AddAsync(userSkill);
        }

        await _dbContext.SaveChangesAsync();
        user.AddSkills(skill);
        return user;
    }

    public Task<List<UserSkills>?> GetUserWithSkills(Guid id)
    {
        var userSkills = _userSkills.AsNoTracking()
             .Where(u => u.IdUser == id)
             .ToListAsync();

        return userSkills;
    }

    public async Task UpdatePassword(Guid userId, string oldPassword, string newPassword)
    {
        var user = await _user
            .SingleOrDefaultAsync(u => u.Id == userId);
        if (user == null)
            throw new NotFoundException();

        var verifyNewPasswordWithOld = oldPassword == newPassword;
        if (verifyNewPasswordWithOld)
            throw new EntityValidationExceptions("the password not match the security policies");
        var oldPasswordHash = ComputeSha256Hash(oldPassword);
        var newPasswordHash = ComputeSha256Hash(newPassword);

        var verifyOldPassword = oldPasswordHash == user.Password;
        if (!verifyOldPassword)
            throw new EntityValidationExceptions("the password not match the security policies");


        user.UpdatePassword(oldPasswordHash, newPasswordHash);

        _user.Update(user);
        await _dbContext.SaveChangesAsync();


    }

    private string ComputeSha256Hash(string password)
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
