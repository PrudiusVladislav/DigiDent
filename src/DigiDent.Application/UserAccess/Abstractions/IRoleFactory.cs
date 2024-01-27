using DigiDent.Domain.SharedKernel.ReturnTypes;
using DigiDent.Domain.SharedKernel.ValueObjects;

namespace DigiDent.Application.UserAccess.Abstractions;

/// <summary>
/// Factory that contains methods for creating and managing <see cref="Role"/> objects.
/// </summary>
public interface IRoleFactory
{
    public Result<Role> CreateRole(string role, params Role[] allowedRoles);
    public Role[] GetEmployeeRoles();
}

