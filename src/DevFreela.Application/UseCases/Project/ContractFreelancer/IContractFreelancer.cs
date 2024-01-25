
using DevFreela.Application.UseCases.Project.Common;
using MediatR;

namespace DevFreela.Application.UseCases.Project.ContractFreelancer;
public interface IContractFreelancer : IRequestHandler<ContractFreelancerInput, ProjectModelOutput>
{
}
