using DevFreela.Application.UseCases.Project.Common;
using MediatR;

namespace DevFreela.Application.UseCases.Project.CreateProject;
public class CreateProjectInput : IRequest<ProjectModelOutput>
{
    public CreateProjectInput(
        string title,
        string description,
        decimal totalCost,
        Guid idClient
        )
    {
        Title = title;
        Description = description;
        IdClient = idClient;
        TotalCost = totalCost;

    }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid IdClient { get; set; }
    public decimal TotalCost { get; set; }
}
