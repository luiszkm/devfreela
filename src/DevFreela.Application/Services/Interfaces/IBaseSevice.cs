

using DevFreela.Application.InputModels;

namespace DevFreela.Application.Services.Interfaces;
public interface IBaseSevice
{
    Guid Create(NewProjectInputModel inputModel);
    void Update(UpdateProjectInputModel inputModel);
    void Delete(Guid id);
}
