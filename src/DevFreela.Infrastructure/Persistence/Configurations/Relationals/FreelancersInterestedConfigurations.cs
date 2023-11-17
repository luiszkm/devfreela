using DevFreela.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Persistence.Configurations.Relationals;
public class FreelancersInterestedConfigurations :
    IEntityTypeConfiguration<FreelancersInterested>
{
    public void Configure(EntityTypeBuilder<FreelancersInterested> builder)
    {
        builder.HasKey(relation => new
        {
            relation.IdFreelancer,
            relation.IdProject
        });
    }
}
