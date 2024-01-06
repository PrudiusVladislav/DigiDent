using DigiDent.Domain.SharedKernel;

namespace DigiDent.Domain.UserAccessContext.Users.Errors;

public static class PasswordErrors
{
    public static Error PasswordIsTooShort(int minLength) 
        => new (ErrorType.Validation,
            $"Password is too short. Minimum length is {minLength}.");
    
    public static Error PasswordIsTooLong(int maxLength) 
        => new (ErrorType.Validation,
            $"Password is too long. Try to keep it under {maxLength} characters.");
    
    public static Error PasswordIsTooWeak
        => new (ErrorType.Validation,
            "Password is too weak. Try to use a stronger password " + "\n" + 
            "(Use a stronger combination of letters, numbers and spec. symbols)."); 
    
}