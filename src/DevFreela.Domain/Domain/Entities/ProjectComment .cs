using DevFreela.Domain.Domain.seddwork;

namespace DevFreela.Domain.Domain.Entities;
public class ProjectComment : AggregateRoot
{
    public ProjectComment(string content, Guid idProject, Guid idUser)
    {
        Content = content;
        IdProject = idProject;
        IdUser = idUser;
        CreatedAt = DateTime.Now;
    }

    public string Content { get; private set; }
    public Guid IdProject { get; private set; }
    public Project Project { get; private set; }
    public Guid IdUser { get; private set; }
    public User User { get; private set; }
    public DateTime CreatedAt { get; private set; }


}