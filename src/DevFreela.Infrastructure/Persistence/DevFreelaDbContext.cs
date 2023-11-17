using DevFreela.Domain.Domain.Entities;
using DevFreela.Domain.Domain.Entities.Models;
using DevFreela.Infrastructure.Models;
using DevFreela.Infrastructure.Persistence.Configurations;
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
    //public DbSet<ProjectComment> ProjectComment =>Set<ProjectComment>();

    //public DbSet<FreelancersInterested> FreelancersInterested => Set<FreelancersInterested>();

    //public DbSet<ProjectComments> ProjectComments =>Set<ProjectComments>();\

    public DbSet<UserSkills> UserSkills => Set<UserSkills>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfigurations());
        // modelBuilder.ApplyConfiguration(new ProjectConfigurations());
        modelBuilder.ApplyConfiguration(new SkillConfigurations());
        //modelBuilder.ApplyConfiguration(new ProjectCommentConfigurations());

        //modelBuilder.ApplyConfiguration(new FreelancersInterestedConfigurations());
        modelBuilder.ApplyConfiguration(new UserSkillsConfigurations());
        // modelBuilder.ApplyConfiguration(new ProjectCommentsConfigurations());
    }
}
