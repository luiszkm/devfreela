

namespace DevFreela.Application.UseCases.Session;
public class SessionOutput
{
    public SessionOutput(Guid id, string token)
    {
        USerID = id;
        Token = token;
    }


    public Guid USerID { get; private set; }
    public string Token { get; private set; }

}
