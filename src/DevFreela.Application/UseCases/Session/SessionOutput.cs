

using DevFreela.Domain.Domain.Enums;

namespace DevFreela.Application.UseCases.Session;
public class SessionOutput
{
    public SessionOutput(
        Guid userId,
        string token
        //UserRole userRole
        )
    {
        UserId = userId;
        Token = token;
        //Role = userRole;
    }

    public Guid UserId { get; private set; }
    public string Token { get; private set; }
    //public UserRole Role { get; private set; }

    public static SessionOutput FromUser(Guid userId, string token)
    {
        return new SessionOutput(userId, token);
    }

}
