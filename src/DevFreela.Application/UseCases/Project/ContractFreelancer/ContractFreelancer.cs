

using DevFreela.Application.Exceptions;
using DevFreela.Application.UseCases.Project.Common;
using DevFreela.Domain.Domain.Enums;
using DevFreela.Domain.Domain.Repository;

namespace DevFreela.Application.UseCases.Project.ContractFreelancer;
public class ContractFreelancer : IContractFreelancer
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUserRepository _userRepository;

    public ContractFreelancer(
        IProjectRepository projectRepository,
        IUserRepository userRepository)
    {
        _projectRepository = projectRepository;
        _userRepository = userRepository;
    }

    public async Task<ProjectModelOutput> Handle(
        ContractFreelancerInput request,
        CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetById(request.IdProject, cancellationToken);
        if (project == null) throw new NotFoundException();

        var freelancer = await _userRepository.GetById(request.IdFreelancer, cancellationToken);
        if (freelancer == null) throw new NotFoundException();

        if (project.IdClient == freelancer.Id)
            throw new BadRequestException("Não é possivel contratar seu propio projeto");

        project.AddFreelancersInterested(freelancer);
        project.ChangeStatus(ProjectStatusEnum.InProgress);
        project.ContractFreelancer(freelancer.Id);

        await _projectRepository.ContractFreelancer(
            project.Id,
            freelancer.Id,
            cancellationToken);


        return ProjectModelOutput.FromProject(project);


    }
}
