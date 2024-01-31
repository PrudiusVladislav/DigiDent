using DigiDent.Shared.Domain.ReturnTypes;
using DigiDent.Shared.Domain.ValueObjects;

namespace DigiDent.Domain.UserAccessContext.Users.Errors;

public static class RoleErrors
{
    public static Error RoleIsNotValid(string roleName)
        => new(
            ErrorType.Validation,
            nameof(Role),
            $"Role {roleName} is not valid.");

    public static Error RoleIsNotAllowed(string roleName)
        => new(
            ErrorType.Validation,
            nameof(Role),
            $"Role {roleName} is not allowed.");
}