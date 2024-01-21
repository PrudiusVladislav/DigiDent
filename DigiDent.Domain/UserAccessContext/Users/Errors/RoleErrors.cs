using DigiDent.Domain.SharedKernel.ReturnTypes;
using DigiDent.Domain.SharedKernel.ValueObjects;

namespace DigiDent.Domain.UserAccessContext.Users.Errors;

public static class RoleErrors
{
    public static Error RoleIsNotValid(string roleName)
        => new(
            ErrorType.Validation,
            nameof(Role),
            $"Role {roleName} is not valid.");   
}