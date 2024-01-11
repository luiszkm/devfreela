
using DevFreela.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Persistence.Configurations.Relationals;
public class UserOwnedProjectsConfigurations : IEntityTypeConfiguration<UserOwnedProjects>
{
    public void Configure(EntityTypeBuilder<UserOwnedProjects> builder)
    {
        builder.HasKey(relation => new
        {
            relation.IdProject,
            relation.IdUser
        });
    }
}
