

using DevFreela.Application.InputModels;
using DevFreela.Application.ViewModels;

namespace DevFreela.Application.Services.Interfaces;
public interface IUserService
{
    Guid Create(NewUserInputModel inputModel);
    void Update(NewUserInputModel inputModel);
    UserViewModel GetById(Guid id);
}
