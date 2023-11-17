using DevFreela.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Persistence.Configurations.Relationals;
public class ProjectCommentsConfigurations : IEntityTypeConfiguration<ProjectComments>
{
    public void Configure(EntityTypeBuilder<ProjectComments> builder)
    {
        builder.HasKey(relation => new
        {
            relation.UserId,
            relation.ProjectId
        });
    }
}
