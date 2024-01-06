using DigiDent.Domain.SharedKernel;

namespace DigiDent.Domain.UserAccessContext.Permissions;

public static class PermissionErrors
{
    public static Error PermissionIsNotValid(string permissionName)
        => new (ErrorType.Validation, $"Permission {permissionName} is not valid.");
}