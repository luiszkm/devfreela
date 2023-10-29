using DevFreela.Application.UseCases.Project.Common;
using MediatR;

namespace DevFreela.Application.UseCases.Project.UpdateProject;
public interface IUpdateProject : IRequestHandler<UpdateProjectInput, ProjectModelOutput>
{
}
