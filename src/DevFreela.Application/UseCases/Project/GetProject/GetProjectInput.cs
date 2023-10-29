
using DevFreela.Application.UseCases.Project.Common;
using DevFreela.Application.ViewModels;
using MediatR;

namespace DevFreela.Application.UseCases.Project.GetProject;
public class GetProjectInput : IRequest<ProjectModelOutput>
{
    public GetProjectInput(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; private set; }
}
