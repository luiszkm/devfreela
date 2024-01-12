using DevFreela.Domain.Domain.Entities;
using DevFreela.Domain.Domain.Enums;
using DevFreela.Domain.Domain.seddwork;
using DevFreela.Domain.Domain.seddwork.SearchbleRepository.cs;

namespace DevFreela.Domain.Domain.Repository;
public interface IProjectRepository :
    IGenericRepository<Project>,
    ISearchRepository<Project>
{
    Task ChangeStatus(Guid id, ProjectStatusEnum newStatus, CancellationToken cancellationToken);
    Task AddFreelancerInterested(Guid projectId, Guid FreelancerId, CancellationToken cancellationToken);

    Task RemoveFreelancerInterested(Guid projectId, Guid FreelancerId, CancellationToken cancellationToken);

    Task ListFreelancersInterested(Guid projectId, CancellationToken cancellationToken);

    // void AddComment(AddCommentInputModel inputModel);
}
