using Microsoft.AspNetCore.Authorization;

namespace DigiDent.UserAccess.Infrastructure.Authorization.RolesRequirement;

public class RolesAuthorizationRequirement: IAuthorizationRequirement
{
    public string[] AllowedRoles { get; }

    public RolesAuthorizationRequirement(params string[] allowedRoles)
    {
        AllowedRoles = allowedRoles ?? throw new ArgumentNullException(nameof(allowedRoles));
    }
}