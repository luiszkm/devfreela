
using DevFreela.Application.UseCases.Project.Common;
using DevFreela.Application.ViewModels;
using MediatR;

namespace DevFreela.Application.UseCases.Project.GetProject;
public interface IGetProject : IRequestHandler<GetProjectInput, ProjectModelOutput>
{
}
