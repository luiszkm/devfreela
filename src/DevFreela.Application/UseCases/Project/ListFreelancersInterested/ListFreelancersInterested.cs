
using DevFreela.Application.Exceptions;
using DevFreela.Application.UseCases.Project.Common;
using DevFreela.Domain.Domain.Repository;
using MediatR;

namespace DevFreela.Application.UseCases.Project.ListFreelancersInterested;
public class ListFreelancersInterested : IRequestHandler<ListFreelancersInterestedInput, ListFreelancers>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUserRepository _userRepository;

    public ListFreelancersInterested(
        IProjectRepository projectRepository,
        IUserRepository userRepository)
    {
        _projectRepository = projectRepository;
        _userRepository = userRepository;
    }

    public async Task<ListFreelancers> Handle(
        ListFreelancersInterestedInput request,
        CancellationToken cancellationToken)
    {
        var project = await _projectRepository
            .GetById(request.IdProject, cancellationToken);

        if (project == null) throw new NotFoundException();

        var freelancers = project.FreelancersInterested;

        return ListFreelancers.FromFreelancer(freelancers);

    }
}
