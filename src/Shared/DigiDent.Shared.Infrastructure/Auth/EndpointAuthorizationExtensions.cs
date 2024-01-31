using DigiDent.Shared.Domain.ValueObjects;
using Microsoft.AspNetCore.Builder;

namespace DigiDent.Shared.Infrastructure.Auth;

public static class EndpointAuthorizationExtensions
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
    
    /// <summary>
    /// Endpoint extension used to require a user to have the same id as the id specified in query.
    /// </summary>
    /// <param name="endpoint"> The endpoint to require id on.</param>
    /// <param name="id">The id to require.</param>
    public static void RequireSameId(
        this IEndpointConventionBuilder endpoint,
        string id)
    {
        endpoint.RequireAuthorization(policy => policy
            .RequireClaim(CustomClaims.NameIdentifier, id));
    }
    
    /// <summary>
    /// Endpoint extension used to require a user to have one of the specified roles or the same id as the id specified in query.
    /// </summary>
    /// <param name="endpoint"> The endpoint to require roles or id on.</param>
    /// <param name="id">The id to require.</param>
    /// <param name="roles">The roles to require. Must be of type <see cref="Role"/>.</param>
    public static void RequireRolesOrSameId(
        this IEndpointConventionBuilder endpoint,
        string id,
        params Role[] roles)
    {
        endpoint.RequireAuthorization(policy => policy
            .RequireAssertion(context =>
            {
                var userRoles = context.User
                    .FindAll(CustomClaims.Role)
                    .Select(c => c.Value);
                
                var hasAllowedRoles = userRoles.Any(role => 
                    roles.Select(r => r.ToString())
                        .Contains(role));
                
                var hasSameId = context.User.Claims.FirstOrDefault(
                    claim => claim.Type == CustomClaims.NameIdentifier)?.Value == id;
                
                return hasAllowedRoles || hasSameId;
            }));
    }
}