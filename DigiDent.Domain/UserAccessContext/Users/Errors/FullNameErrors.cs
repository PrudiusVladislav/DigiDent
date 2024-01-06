using DigiDent.Domain.SharedKernel;

namespace DigiDent.Domain.UserAccessContext.Users.Errors;

public static class FullNameErrors
{
    public static Error FirstNameIsEmpty 
        => new(ErrorType.Validation, "First name cannot be empty");
    
    public static Error FirstNameHasInvalidLength(int minLength, int maxLength) 
        => new(ErrorType.Validation,
            $"First name should be between {minLength} and {maxLength} characters");
    
    public static Error LastNameIsEmpty 
        => new(ErrorType.Validation, "Last name cannot be empty");
    
    public static Error LastNameHasInvalidLength(int minLength, int maxLength)
        => new(ErrorType.Validation,
            $"Last name should be between {minLength} and {maxLength} characters");
}