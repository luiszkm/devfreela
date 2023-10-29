

namespace DevFreela.Application.InputModels;
public class AddCommentInputModel
{
    public string Content { get; set; }
    public Guid IdUser { get; set; }
    public Guid IdProject { get; set; }
}
