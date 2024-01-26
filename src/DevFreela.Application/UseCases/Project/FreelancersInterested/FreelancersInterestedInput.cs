

using DevFreela.Application.UseCases.Project.Common;
using MediatR;

namespace DevFreela.Application.UseCases.Project.FreelancersInterested;
public class FreelancersInterestedInput : IRequest<ProjectModelOutput>
{
    public FreelancersInterestedInput(Guid idProject,
        Guid idFreelancer,
        bool favotire = true)
    {
        IdProject = idProject;
        IdFreelancer = idFreelancer;
        Favotire = favotire;
    }

    public Guid IdProject { get; set; }
    public Guid IdFreelancer { get; set; }
    public bool Favotire { get; set; }
}
