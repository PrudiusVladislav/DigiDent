using DigiDent.Shared.Kernel.ReturnTypes;
using DigiDent.Shared.Kernel.ValueObjects;

namespace DigiDent.Shared.Kernel.Errors;

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