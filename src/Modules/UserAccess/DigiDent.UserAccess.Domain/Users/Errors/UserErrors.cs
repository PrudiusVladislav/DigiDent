using DigiDent.Shared.Kernel.ReturnTypes;
using DigiDent.UserAccess.Domain.Users.ValueObjects;

namespace DigiDent.UserAccess.Domain.Users.Errors;

public static class UserErrors
{
    public static Error UserDoesNotExist(UserId userId)
        => new(
            ErrorType.NotFound,
            nameof(User),
            $"User with id '{userId.Value}' does not exist.");
    
    public static Error CannotDeleteLastAdmin
        => new(
            ErrorType.Validation,
            nameof(User),
            "Cannot delete last administrator.");
}