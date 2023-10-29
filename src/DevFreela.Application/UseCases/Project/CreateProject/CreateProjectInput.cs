using DevFreela.Application.UseCases.Project.Common;
using MediatR;

namespace DevFreela.Application.UseCases.Project.CreateProject;
public class CreateProjectInput : IRequest<ProjectModelOutput>
{
    public CreateProjectInput(
        string title,
        string description,
        Guid idClient,
        Guid idFreelancer,
        decimal totalCost
        )
    {
        Title = title;
        Description = description;
        IdClient = idClient;
        IdFreelancer = idFreelancer;
        TotalCost = totalCost;

    }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid IdClient { get; set; }
    public Guid IdFreelancer { get; set; }
    public decimal TotalCost { get; set; }
}
