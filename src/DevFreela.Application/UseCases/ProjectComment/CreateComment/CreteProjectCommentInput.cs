using DevFreela.Application.UseCases.ProjectComment.Common;
using MediatR;

namespace DevFreela.Application.UseCases.ProjectComment.CreateComment;
public class CreteProjectCommentInput : IRequest<CreteProjectCommentModelOutput>
{
    public CreteProjectCommentInput(
        string content,
        Guid idUser,
        Guid idProject)
    {
        Content = content;
        IdUser = idUser;
        IdProject = idProject;
    }
    public string Content { get; set; }
    public Guid IdUser { get; set; }
    public Guid IdProject { get; set; }
}
