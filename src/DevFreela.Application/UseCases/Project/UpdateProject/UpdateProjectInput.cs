
using DevFreela.Application.UseCases.Project.Common;
using MediatR;

namespace DevFreela.Application.UseCases.Project.UpdateProject;
public class UpdateProjectInput : IRequest<ProjectModelOutput>
{
    public UpdateProjectInput(
        Guid id,
        string title,
        string description,
        decimal totalCost)
    {
        Id = id;
        Title = title;
        Description = description;
        TotalCost = totalCost;
    }
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal TotalCost { get; set; }
}
