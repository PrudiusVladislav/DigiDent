using DigiDent.Domain.SharedKernel;

namespace DigiDent.Domain.UserAccessContext.Roles;

public static class RoleErrors
{
    public static Error RoleIsNotValid(string value)
        => new (ErrorType.Validation, $"The role {value} does not exist.");
}