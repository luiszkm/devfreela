

using DevFreela.Domain.Domain.Exceptions;

namespace DevFreela.Domain.Domain.Validation;
public class DomainValidation
{

    public static void NotNull(object? target, string fieldName)
    {
        if (target is null) throw new EntityValidationExceptions($"{fieldName} should not be null");
    }

    public static void NotNullOrEmpty(string? target, string fieldName)
    {
        if (String.IsNullOrWhiteSpace(target))
            throw new EntityValidationExceptions($"{fieldName} should not be empty or null");
    }

    public static void MinLength(string target, int minLength, string fieldName)
    {
        if (target.Length < minLength) throw new EntityValidationExceptions($"{fieldName} should be at {minLength} characters long");

    }

    public static void MaxLength(string target, int maxLength, string fieldName)
    {
        if (target.Length > maxLength) throw new EntityValidationExceptions($"{fieldName} should be less or equal {maxLength} characters long");
    }
}

