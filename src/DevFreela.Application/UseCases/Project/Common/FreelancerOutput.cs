
namespace DevFreela.Application.UseCases.Project.Common;
public class FreelancerOutput
{
    public FreelancerOutput(
        Guid userId,
        string name,
        string email,
        string? avatar = null)
    {
        UserId = userId;
        Name = name;
        Email = email;
        Avatar = avatar;
    }


    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string? Avatar { get; set; }



}
