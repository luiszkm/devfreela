using DevFreela.Domain.Domain.Entities;
using DevFreela.Domain.Domain.Entities.Models;
using DevFreela.Infrastructure.Models;
using DevFreela.Infrastructure.Persistence.Configurations;
using DevFreela.Infrastructure.Persistence.Configurations.Relationals;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence;
public class DevFreelaDbContext : DbContext
{
    public DevFreelaDbContext(DbContextOptions<DevFreelaDbContext> options) :
        base(options)
    {

    }
    public DbSet<User> Users =>
        Set<User>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Skill> Skills => Set<Skill>();


    /// Relational Tables »»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»
    public DbSet<UserOwnedProjects> UserOwnedProjects => Set<UserOwnedProjects>();
    public DbSet<FreelancerOwnedProjects> FreelancerOwnedProjects => Set<FreelancerOwnedProjects>();

    //public DbSet<ProjectComment> ProjectComment =>Set<ProjectComment>();

    public DbSet<FreelancersInterested> FreelancersInterested => Set<FreelancersInterested>();

    //public DbSet<ProjectComments> ProjectComments =>Set<ProjectComments>();\

    public DbSet<UserSkills> UserSkills => Set<UserSkills>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfigurations());
        modelBuilder.ApplyConfiguration(new ProjectConfigurations());
        modelBuilder.ApplyConfiguration(new SkillConfigurations());
        //modelBuilder.ApplyConfiguration(new ProjectCommentConfigurations());
        // Relational Tab »»»»»»»»»»»»
        modelBuilder.ApplyConfiguration(new FreelancersInterestedConfigurations());
        modelBuilder.ApplyConfiguration(new UserSkillsConfigurations());
        modelBuilder.ApplyConfiguration(new UserOwnedProjectsConfigurations());
        modelBuilder.ApplyConfiguration(new FreelancerOwnedProjectsConfigurations());
        // modelBuilder.ApplyConfiguration(new ProjectCommentsConfigurations());
    }
}
