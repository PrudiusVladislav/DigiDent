using DigiDent.Shared.Kernel.ReturnTypes;
using DigiDent.Shared.Kernel.ValueObjects;

namespace DigiDent.UserAccess.Domain.Users.Errors;

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