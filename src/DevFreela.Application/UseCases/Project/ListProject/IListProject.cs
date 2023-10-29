
using MediatR;

namespace DevFreela.Application.UseCases.Project.ListProject;
public interface IListProject : IRequestHandler<ListInputProject, ListProjectOutput>
{
}
