using DigiDent.Domain.SharedKernel;

namespace DigiDent.Application.UserAccess.Tokens;

public static class TokensErrors
{
    public static Error InvalidToken =>
        new(ErrorType.Unauthorized, "Invalid token.");
    
    public static Error TokenIsNotExpired =>
        new(ErrorType.Conflict, "This token hasn't expired yet.");
    
    public static Error RefreshTokenDoesNotExist =>
        new(ErrorType.NotFound, "This refresh token doesn't exist.");
    
    public static Error RefreshTokenExpired =>
        new(ErrorType.Conflict, "This refresh token has expired.");
    
    public static Error RefreshTokenIsInvalidated =>
        new(ErrorType.Conflict, "This refresh token has been invalidated.");
    
}