
using DevFreela.Domain.Domain.Entities;
using DevFreela.Domain.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repository;
public class ProjectCommentRepository : IProjectCommentRepository
{
    private readonly DevFreelaDbContext _dbContext;

    public ProjectCommentRepository(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private DbSet<ProjectComment> _projectComment => _dbContext.Set<ProjectComment>();


    public async Task Create(ProjectComment aggregate, CancellationToken cancellationToken)
    => await _projectComment.AddAsync(aggregate, cancellationToken);

    public Task<ProjectComment> GetById(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task Update(ProjectComment aggregate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task Delete(ProjectComment aggregate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
