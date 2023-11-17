

using DevFreela.Domain.Domain.Entities;

namespace DevFreela.Infrastructure.Models;
public class FreelancersInterested
{

    public FreelancersInterested(
        Guid idProject,
        Guid idFreelancer
        )
    {
        IdProject = idProject;
        IdFreelancer = idFreelancer;



    }

    public Guid IdProject { get; set; }
    public Project Project { get; set; }
    public Guid IdFreelancer { get; set; }
    public User Freelancer { get; set; }

}
