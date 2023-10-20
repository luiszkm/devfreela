

namespace DevFreela.Domain.Domain.Exceptions;
public class EntityValidationExceptions : Exception
{
    public EntityValidationExceptions(string? message) : base(message)
    {
    }
}
