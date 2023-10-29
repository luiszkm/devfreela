
using DevFreela.Domain.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace DevFreela.Infrastructure.Configurations;
internal class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
          .HasKey(u => u.Id);

        builder
            .HasMany(u => u.Skills)
            .WithOne()
            .HasForeignKey(u => u.IdSkill)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
