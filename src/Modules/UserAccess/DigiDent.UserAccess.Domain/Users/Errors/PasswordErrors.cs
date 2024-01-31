using DigiDent.Shared.Domain.ReturnTypes;
using DigiDent.UserAccess.Domain.Users.ValueObjects;

namespace DigiDent.UserAccess.Domain.Users.Errors;

public static class PasswordErrors
{
    public static Error PasswordIsTooShort(int minLength) 
        => new (
            ErrorType.Validation,
            nameof(Password),
            $"Password is too short. Minimum length is {minLength}.");
    
    public static Error PasswordIsTooLong(int maxLength) 
        => new (
            ErrorType.Validation,
            nameof(Password),
            $"Password is too long. Try to keep it under {maxLength} characters.");
    
    public static Error PasswordIsTooWeak
        => new (
            ErrorType.Validation,
            nameof(Password),
            "Password is too weak. Try to use a stronger password " + 
            "(Use a stronger combination of letters, numbers and spec. symbols)."); 
    
    public static Error PasswordDoesNotMatch 
        => new (
            ErrorType.NotFound,
            nameof(Password),
            "Entered password does not match the password in the database.");
}