
using MediatR;

namespace DevFreela.Application.UseCases.Project.DeleteProject;
public class DeleteProjectInput : IRequest
{
    public DeleteProjectInput(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
