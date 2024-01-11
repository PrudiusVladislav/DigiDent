using DigiDent.Domain.SharedKernel.Abstractions;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Domain.UserAccessContext.Users.Errors;

public static class RoleErrors
{
    public static Error RoleIsNotValid(string roleName)
        => new(ErrorType.Validation, $"Role {roleName} is not valid.");   
}