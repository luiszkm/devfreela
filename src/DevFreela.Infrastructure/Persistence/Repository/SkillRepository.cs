

using DevFreela.Domain.Domain.Entities;
using DevFreela.Domain.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repository;
public class SkillRepository : ISkillRepository
{
    private readonly DevFreelaDbContext _dbContext;

    public SkillRepository(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    private DbSet<Skill> _skills => _dbContext.Set<Skill>();


    public List<Skill> GetAll()
    {
        return _skills.ToList();
    }
}
