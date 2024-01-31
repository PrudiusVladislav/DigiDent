using DigiDent.Shared.Domain.ReturnTypes;

namespace DigiDent.UserAccess.Application.Tokens;

public static class TokensErrors
{
    public static Error InvalidToken =>
        new (
            ErrorType.Validation,
            "Token",
            "Invalid token.");
    
    public static Error TokenIsNotExpired =>
        new (
            ErrorType.Conflict,
            "Token",
            "This token hasn't expired yet.");
    
}