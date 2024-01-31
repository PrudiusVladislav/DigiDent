﻿using DigiDent.Application.UserAccess.Abstractions;
using DigiDent.Shared.Domain.ReturnTypes;
using DigiDent.Shared.Domain.ValueObjects;
using DigiDent.Domain.UserAccessContext.Users.Errors;

namespace DigiDent.Infrastructure.UserAccess.Authorization;

public class RoleFactory: IRoleFactory
{
    public Result<Role> CreateRole(
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
    
    public Role[] GetEmployeeRoles() =>
    [
        Role.Administrator,
        Role.Doctor,
        Role.Assistant
    ];
}