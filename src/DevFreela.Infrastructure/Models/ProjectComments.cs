

using DevFreela.Domain.Domain.Entities;

namespace DevFreela.Infrastructure.Models;
public class ProjectComments
{
    public ProjectComments(
        Guid userId,
        Guid projectId)
    {
        UserId = userId;
        ProjectId = projectId;
    }

    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid ProjectId { get; set; }
    public Project Project { get; set; }
}
