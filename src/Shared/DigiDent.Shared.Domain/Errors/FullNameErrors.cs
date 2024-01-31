using DigiDent.Shared.Domain.ReturnTypes;
using DigiDent.Shared.Domain.ValueObjects;

namespace DigiDent.Shared.Domain.Errors;

public static class FullNameErrors
{
    public static Error NameIsEmpty(string propertyName)
        => new(
            ErrorType.Validation,
            nameof(FullName),
            $"{propertyName} cannot be empty");
    
    public static Error NameHasInvalidLength(
        string propertyName, int minLength, int maxLength) 
        => new(
            ErrorType.Validation,
            nameof(FullName),
            $"{propertyName} should be between {minLength} and {maxLength} characters");
}