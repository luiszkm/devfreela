

using DevFreela.Domain.Domain.Entities;
using DevFreela.Domain.Domain.Enums;
using DevFreela.Domain.Domain.Repository;
using DevFreela.Domain.Domain.seddwork.SearchbleRepository.cs;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repository;
public class ProjectRepository : IProjectRepository
{
    private readonly DevFreelaDbContext _dbContext;

    public ProjectRepository(DevFreelaDbContext dbContext)
        => _dbContext = dbContext;

    private DbSet<Project> _projects => _dbContext.Set<Project>();

    public async Task Create(Project aggregate, CancellationToken cancellationToken)
    {
        await _projects.AddAsync(aggregate, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Project> GetById(Guid id, CancellationToken cancellationToken)
    {
        var project = await _projects.SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return project;
    }

    public async Task Update(Project aggregate, CancellationToken cancellationToken)
    {
        var project = await _projects.SingleOrDefaultAsync(p => p.Id == aggregate.Id, cancellationToken);
        if (project == null)
        {
            return;
        }
        project.Update(aggregate.Title, aggregate.Description, aggregate.TotalCost);
        await _dbContext.SaveChangesAsync(cancellationToken);



    }

    public async Task Delete(Project aggregate, CancellationToken cancellationToken)
    {
        var project = await _projects.SingleOrDefaultAsync(p => p.Id == aggregate.Id, cancellationToken);
        if (project == null)
        {
            return;
        }
        _projects.Remove(project);
        await _dbContext.SaveChangesAsync(cancellationToken);

    }

    public Task<SearchOutput<Project>> Search(SearchInput input, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task ChangeStatus(Guid id, ProjectStatusEnum newStatus, CancellationToken cancellationToken)
    {
        var project = await _projects.SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
        if (project == null)
        {
            return;
        }
        project.ChangeStatus(newStatus);
        await _dbContext.SaveChangesAsync(cancellationToken);


    }


}
