

using DevFreela.Application.UseCases.Project.Common;
using MediatR;

namespace DevFreela.Application.UseCases.Project.ListFreelancersInterested;
public class ListFreelancersInterestedInput : IRequest<ListFreelancers>
{
    public ListFreelancersInterestedInput(Guid idProject)
    {
        IdProject = idProject;
    }

    public Guid IdProject { get; set; }
}
