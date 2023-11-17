

using MediatR;

namespace DevFreela.Application.UseCases.Session;
public class SessionInput : IRequest<SessionOutput>
{
    public SessionInput(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public string Email { get; set; }
    public string Password { get; set; }
}
