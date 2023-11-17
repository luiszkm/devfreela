using DevFreela.Domain.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Persistence.Configurations;
internal class ProjectConfigurations : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder
            .HasKey(p => p.Id);

        builder
            .HasMany(p => p.Comments)
            .WithOne()
            .HasForeignKey(p => p.IdProject)
            .OnDelete(DeleteBehavior.Restrict);


    }
}
