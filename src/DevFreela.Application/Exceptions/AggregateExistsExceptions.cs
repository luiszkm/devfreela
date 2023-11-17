

namespace DevFreela.Application.Exceptions;
public class AggregateExistsExceptions : ApplicationException
{
    public AggregateExistsExceptions(string message) : base(message)
    {
    }
}
