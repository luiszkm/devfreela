

using DevFreela.Application.Exceptions;
using DevFreela.Application.UseCases.Project.Common;
using DevFreela.Domain.Domain.Repository;

namespace DevFreela.Application.UseCases.Project.FreelancersInterested;
public class FreelancersInterested : IFreelancersInterested
{
    private readonly IProjectRepository _projectRepository;

    private readonly IUserRepository _userRepository;

    public FreelancersInterested(IProjectRepository projectRepository,
        IUserRepository userRepository)
    {
        _projectRepository = projectRepository;
        _userRepository = userRepository;
    }

    public async Task<ProjectModelOutput>
        Handle(FreelancersInterestedInput request,
            CancellationToken cancellationToken)
    {

        var project = await _projectRepository
            .GetById(request.IdProject, cancellationToken);

        if (project == null)
        {
            throw new NotFoundException();
        }

        var freelancer = await _userRepository
            .GetById(request.IdFreelancer, cancellationToken);

        if (freelancer == null)
        {
            throw new NotFoundException();
        }

        if (request.Favotire)
        {
            project.AddFreelancersInterested(freelancer);

            await _projectRepository.AddFreelancerInterested(
                project.Id,
                freelancer.Id,
                cancellationToken);
        }
        else
        {
            project.RemoveFreelancersInterested(freelancer);

            await _projectRepository.RemoveFreelancerInterested(
                               project.Id,
                               freelancer.Id,
                               cancellationToken);
        }


        return ProjectModelOutput.FromProject(project);


    }
}
