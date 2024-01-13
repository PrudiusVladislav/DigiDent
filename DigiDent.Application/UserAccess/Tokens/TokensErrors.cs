using DigiDent.Domain.SharedKernel.Abstractions;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Application.UserAccess.Tokens;

public static class TokensErrors
{
    public static Error InvalidToken =>
        new(
            ErrorType.Validation,
            "Token",
            "Invalid token.");
    
    public static Error TokenIsNotExpired =>
        new(
            ErrorType.Conflict,
            "Token",
            "This token hasn't expired yet.");
    
}