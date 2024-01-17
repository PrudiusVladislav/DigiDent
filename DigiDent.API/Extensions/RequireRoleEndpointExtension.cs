using DigiDent.Domain.SharedKernel.ValueObjects;

namespace DigiDent.API.Extensions;

public static class RequireRoleEndpointExtension
{
    /// <summary>
    /// Endpoint extension method to require a user to have one of the specified roles.
    /// </summary>
    /// <param name="endpoint">The endpoint to require roles on.</param>
    /// <param name="roles">The roles to require. Must be of type <see cref="Role"/>.</param>
    public static void RequireRoles(
        this IEndpointConventionBuilder endpoint,
        params Role[] roles)
    {
        endpoint.RequireAuthorization(policy => policy
            .RequireRole(roles
                .Select(r => r.ToString())
                .ToArray()));
    }
}