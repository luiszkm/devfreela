

using DevFreela.Application.Exceptions;
using DevFreela.Domain.Domain.Entities;
using DevFreela.Domain.Domain.Enums;
using DevFreela.Domain.Domain.Repository;
using DevFreela.Domain.Domain.seddwork.SearchbleRepository.cs;
using DevFreela.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repository;

public class ProjectRepository : IProjectRepository
{
    private readonly DevFreelaDbContext _dbContext;

    public ProjectRepository(DevFreelaDbContext dbContext)
        => _dbContext = dbContext;

    private DbSet<Project> _projects => _dbContext.Set<Project>();
    private DbSet<Skill> _skills => _dbContext.Set<Skill>();

    private DbSet<User> _user => _dbContext.Set<User>();

    private DbSet<UserOwnedProjects> _userOwnedProjects =>
        _dbContext.Set<UserOwnedProjects>();
    private DbSet<FreelancerOwnedProjects> _freelancerOwnedProjects =>
        _dbContext.Set<FreelancerOwnedProjects>();

    private DbSet<FreelancersInterested> _freelancersInterested =>
        _dbContext.Set<FreelancersInterested>();

    public async Task Create(Project aggregate, CancellationToken cancellationToken)
    {
        await _projects.AddAsync(aggregate, cancellationToken);
        await _userOwnedProjects.AddAsync(new UserOwnedProjects(aggregate.Id, aggregate.IdClient), cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Project> GetById(Guid id, CancellationToken cancellationToken)
    {
        var project = await _projects.AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
        if (project == null)
            throw new NotFoundException();

        var freelancerInterested = await _freelancersInterested
            .Where(f => f.IdProject == id)
            .ToListAsync(cancellationToken);

        foreach (var freelancer in freelancerInterested)
        {
            var userFreelancer = await _user
                .SingleOrDefaultAsync(u => u.Id == freelancer.IdFreelancer, cancellationToken);

            project.AddFreelancersInterested(userFreelancer);

        }

        var freelancerOwned = await _freelancerOwnedProjects
            .SingleOrDefaultAsync(f => f.IdProject == id, cancellationToken);
        if (freelancerOwned != null)
        {
            var freelancer = await _user
                .SingleOrDefaultAsync(u => u.Id == freelancerOwned.IdUser, cancellationToken);
            project.ContractFreelancer(freelancer.Id);
        }




        return project;
    }

    public async Task Update(
        Project aggregate,
        CancellationToken cancellationToken)
    {
        var project = await _projects.SingleOrDefaultAsync(p => p.Id == aggregate.Id, cancellationToken);
        if (project == null)
            throw new NotFoundException();


        project.Update(aggregate.Title, aggregate.Description, aggregate.TotalCost);
        _projects.Update(project);
        await _dbContext.SaveChangesAsync(cancellationToken);


    }

    public async Task Delete(
        Project aggregate,
        CancellationToken cancellationToken)
    {
        var project = await _projects.SingleOrDefaultAsync(p => p.Id == aggregate.Id, cancellationToken);
        if (project == null)
            throw new NotFoundException();



        _projects.Remove(project);
        await _dbContext.SaveChangesAsync(cancellationToken);

    }

    public Task<SearchOutput<Project>> Search(
        SearchInput input,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task ChangeStatus(
        Guid id,
        ProjectStatusEnum newStatus,
        CancellationToken cancellationToken)
    {
        var project = await _projects.SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
        if (project == null)
        {
            return;
        }

        project.ChangeStatus(newStatus);
        await _dbContext.SaveChangesAsync(cancellationToken);


    }

    public async Task AddFreelancerInterested(
        Guid projectId,
        Guid freelancerId,
        CancellationToken cancellationToken)
    {
        var freelancerInterested = await _user
            .SingleOrDefaultAsync(f => f.Id == freelancerId, cancellationToken);

        if (freelancerInterested == null)
            throw new NotFoundException();

        var project = await _projects
            .SingleOrDefaultAsync(p => p.Id == projectId, cancellationToken);

        if (project == null)
            throw new NotFoundException();

        await _freelancersInterested.AddAsync(new FreelancersInterested(projectId, freelancerId), cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

    }

    public async Task RemoveFreelancerInterested(
        Guid projectId,
        Guid freelancerId,
        CancellationToken cancellationToken)
    {
        var freelancerInterested = await _user
            .SingleOrDefaultAsync(f => f.Id == freelancerId, cancellationToken);

        if (freelancerInterested == null)
            throw new NotFoundException();

        var project = await _projects
            .SingleOrDefaultAsync(p => p.Id == projectId, cancellationToken);

        if (project == null)
            throw new NotFoundException();

        var freelancer = await _freelancersInterested
            .SingleOrDefaultAsync(f => f.IdFreelancer == freelancerId && f.IdProject == projectId, cancellationToken);
        if (freelancer == null)
            throw new NotFoundException();

        _freelancersInterested.Remove(freelancer);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task ContractFreelancer(Guid projectId, Guid FreelancerId, CancellationToken cancellationToken)
    {
        var project = await _projects
            .SingleOrDefaultAsync(p => p.Id == projectId, cancellationToken);

        var freelancer = await _user
            .SingleOrDefaultAsync(f => f.Id == FreelancerId, cancellationToken);

        if (freelancer == null || project == null)
            throw new NotFoundException();


        project.ContractFreelancer(freelancer.Id);

        project.ChangeStatus(ProjectStatusEnum.InProgress);

        freelancer.AddFreelanceProject(project);


        _freelancerOwnedProjects.Add(
           new FreelancerOwnedProjects(
               projectId,
               FreelancerId));

        _user.Update(freelancer);
        _projects.Update(project);



        await _dbContext.SaveChangesAsync(cancellationToken);


    }



}
