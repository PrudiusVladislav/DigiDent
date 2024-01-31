using DigiDent.Domain.SharedKernel.ReturnTypes;
using DigiDent.Domain.SharedKernel.ValueObjects;

namespace DigiDent.Domain.SharedKernel.Errors;

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