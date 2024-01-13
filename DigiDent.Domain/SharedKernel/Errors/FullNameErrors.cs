using DigiDent.Domain.SharedKernel.ReturnTypes;
using DigiDent.Domain.SharedKernel.ValueObjects;

namespace DigiDent.Domain.SharedKernel.Errors;

public static class FullNameErrors
{
    public static Error FirstNameIsEmpty 
        => new(
            ErrorType.Validation,
            nameof(FullName),
            "First name cannot be empty");
    
    public static Error FirstNameHasInvalidLength(int minLength, int maxLength) 
        => new(
            ErrorType.Validation,
            nameof(FullName),
            $"First name should be between {minLength} and {maxLength} characters");
    
    public static Error LastNameIsEmpty 
        => new(
            ErrorType.Validation,
            nameof(FullName),
            "Last name cannot be empty");
    
    public static Error LastNameHasInvalidLength(int minLength, int maxLength)
        => new(
            ErrorType.Validation,
            nameof(FullName),
            $"Last name should be between {minLength} and {maxLength} characters");
}