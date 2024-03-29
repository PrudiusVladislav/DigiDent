﻿using DigiDent.Shared.Kernel.ReturnTypes;
using DigiDent.Shared.Kernel.ValueObjects;

namespace DigiDent.UserAccess.Application.Abstractions;

/// <summary>
/// Factory that contains methods for creating and managing <see cref="Role"/> objects.
/// </summary>
public interface IRoleFactory
{
    public Result<Role> CreateRole(string role, params Role[] allowedRoles);
    public Role[] GetEmployeeRoles();
}

