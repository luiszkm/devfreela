using DevFreela.Application.InputModels;
using DevFreela.Application.ViewModels;

namespace DevFreela.Application.Services.Interfaces;
public interface IProjectServices
{
    List<ProjectViewModel> GetAll(string query);
    ProjectDetailsViewModel GetById(Guid id);
    Guid Create(NewProjectInputModel inputModel);
    void Update(UpdateProjectInputModel inputModel);
    void Delete(Guid id);
    void Start(Guid id);
    void Finish(Guid id);
    void AddComment(AddCommentInputModel inputModel);
}
