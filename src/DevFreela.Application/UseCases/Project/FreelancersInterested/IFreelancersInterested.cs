

using DevFreela.Application.UseCases.Project.Common;
using MediatR;

namespace DevFreela.Application.UseCases.Project.FreelancersInterested;
public interface IFreelancersInterested :
    IRequestHandler<FreelancersInterestedInput, ProjectModelOutput>
{
}
