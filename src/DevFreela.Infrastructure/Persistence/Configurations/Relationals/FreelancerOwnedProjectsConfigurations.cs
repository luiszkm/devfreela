

using DevFreela.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Persistence.Configurations.Relationals;

public class FreelancerOwnedProjectsConfigurations : IEntityTypeConfiguration<FreelancerOwnedProjects>
{
    public void Configure(EntityTypeBuilder<FreelancerOwnedProjects> builder)
    {
        builder.HasKey(relation => new
        {
            relation.IdProject,
            relation.IdUser
        });
    }
}

