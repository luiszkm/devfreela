
using DevFreela.Application.UseCases.Project.Common;
using MediatR;

namespace DevFreela.Application.UseCases.Project.ContractFreelancer;
public class ContractFreelancerInput : IRequest<ProjectModelOutput>
{
    public ContractFreelancerInput(
        Guid idProject,
        Guid idFreelancer)
    {
        IdProject = idProject;
        IdFreelancer = idFreelancer;
    }

    public Guid IdProject { get; set; }
    public Guid IdFreelancer { get; set; }
}
