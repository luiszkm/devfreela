using DevFreela.Domain.Domain.Entities;
using DevFreela.Domain.Domain.seddwork;
using DevFreela.Domain.Domain.seddwork.SearchbleRepository.cs;

namespace DevFreela.Domain.Domain.Repository;
public interface IProjectRepository :
    IGenericRepository<Project>,
    ISearchRepository<Project>
{
    void Start(Guid id);
    void Finish(Guid id);
    // void AddComment(AddCommentInputModel inputModel);
}
