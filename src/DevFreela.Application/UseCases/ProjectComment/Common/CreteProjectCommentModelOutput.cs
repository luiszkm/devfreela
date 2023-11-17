

namespace DevFreela.Application.UseCases.ProjectComment.Common;
public class CreteProjectCommentModelOutput
{
    public CreteProjectCommentModelOutput(
        string content,
        Guid idUser,
        Guid idProject
        )
    {
        Content = content;
        IdUser = idUser;
        IdProject = idProject;
        CreatedAt = DateTime.Now;
    }


    public string Content { get; set; }
    public Guid IdUser { get; set; }
    public Guid IdProject { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public static CreteProjectCommentModelOutput FromProjectComment(DomainEntity.ProjectComment projectComment)
    {
        return new CreteProjectCommentModelOutput(
                       projectComment.Content,
                       projectComment.IdUser,
                       projectComment.IdProject);
    }

}
