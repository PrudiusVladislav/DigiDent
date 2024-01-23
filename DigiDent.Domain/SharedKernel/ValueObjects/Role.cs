using DigiDent.Domain.SharedKernel.ReturnTypes;
using DigiDent.Domain.UserAccessContext.Users.Errors;

namespace DigiDent.Domain.SharedKernel.ValueObjects;

public enum Role
{
    Administrator,
    Doctor,
    Assistant,
    Patient
}

/// <summary>
/// Factory that contains methods for creating and managing <see cref="Role"/> objects.
/// </summary>
public static class RoleFactory
{
    public static Result<Role> CreateRole(
        string roleName, params Role[] allowedRoles)
    {
        var isRoleValid = Enum.TryParse<Role>(roleName, true, out var role);
        if (!isRoleValid)
        {
            return Result.Fail<Role>(RoleErrors
                .RoleIsNotValid(roleName));
        }

        if (allowedRoles.Length > 0 && !allowedRoles.Contains(role))
        {
            return Result.Fail<Role>(RoleErrors
                .RoleIsNotAllowed(roleName));
        }
        
        return Result.Ok(role);
    }
    
    public static Role[] EmployeeRoles => new[]
    {
        Role.Administrator,
        Role.Doctor,
        Role.Assistant
    };
}