using Microsoft.AspNetCore.Authorization;

namespace DigiDent.Infrastructure.UserAccess;

public class RolesAuthorizationRequirement: IAuthorizationRequirement
{
    public string[] AllowedRoles { get; }

    public RolesAuthorizationRequirement(params string[] allowedRoles)
    {
        AllowedRoles = allowedRoles ?? throw new ArgumentNullException(nameof(allowedRoles));
    }
}