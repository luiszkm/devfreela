
using DevFreela.Domain.Domain.Entities;

namespace DevFreela.Infrastructure.Models;
public class FreelancerOwnedProjects
{

    public FreelancerOwnedProjects(
        Guid idProject,
        Guid idUser
    )
    {
        IdProject = idProject;
        IdUser = idUser;
    }


    public Guid IdProject { get; set; }
    public Project? Project { get; set; }
    public Guid IdUser { get; set; }
    public User? User { get; set; }
}
