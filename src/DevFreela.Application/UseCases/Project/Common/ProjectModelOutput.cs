

namespace DevFreela.Application.UseCases.Project.Common;
public class ProjectModelOutput
{
    public ProjectModelOutput(
        Guid id,
        Guid clientId,
        string title,
        string description,
        decimal totalCost,
        DateTime createdAt)
    {
        Id = id;
        ClientId = clientId;
        Title = title;
        Description = description;
        TotalCost = totalCost;
        CreatedAt = createdAt;

    }

    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public decimal TotalCost { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public Guid ClientId { get; private set; }


    public static ProjectModelOutput FromProject(DomainEntity.Project project)
    {
        return new ProjectModelOutput(
            project.Id,
            project.IdClient,
            project.Title,
            project.Description,
            project.TotalCost,
            project.CreatedAt
        );
    }
}
