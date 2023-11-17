
using DevFreela.Application.UseCases.ProjectComment.Common;

namespace DevFreela.Application.UseCases.ProjectComment.CreateComment;
public class CreteProjectComment : ICreteProjectComment
{
    public async Task<CreteProjectCommentModelOutput>
        Handle(CreteProjectCommentInput request, CancellationToken cancellationToken)
    {
        var projectComment = new DomainEntity.ProjectComment(
                       request.Content,
                       request.IdUser,
                       request.IdProject);

        return CreteProjectCommentModelOutput.FromProjectComment(projectComment);

    }
}
