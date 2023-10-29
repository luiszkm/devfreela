

namespace DevFreela.Application.UseCases.Project.Common;
public class ProjectModelOutput
{
    public ProjectModelOutput(
        string title,
        string description,
        decimal totalCost,
        DateTime createdAt)
    {
        Title = title;
        Description = description;
        TotalCost = totalCost;
        CreatedAt = createdAt;
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
    public decimal TotalCost { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public static ProjectModelOutput FromProject(DomainEntity.Project project)
    {
        return new ProjectModelOutput(
            project.Title,
            project.Description,
            project.TotalCost,
            project.CreatedAt
        );
    }
}
