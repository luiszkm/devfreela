using DevFreela.Application.UseCases.ProjectComment.Common;
using MediatR;

namespace DevFreela.Application.UseCases.ProjectComment.CreateComment;
public interface ICreteProjectComment : IRequestHandler<CreteProjectCommentInput, CreteProjectCommentModelOutput>
{
}
