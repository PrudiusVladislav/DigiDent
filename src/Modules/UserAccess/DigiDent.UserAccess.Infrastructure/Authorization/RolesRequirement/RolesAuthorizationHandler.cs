using DigiDent.Shared.Infrastructure.Auth;
using Microsoft.AspNetCore.Authorization;

namespace DigiDent.UserAccess.Infrastructure.Authorization.RolesRequirement;

public class RolesAuthorizationHandler :
    AuthorizationHandler<RolesAuthorizationRequirement>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        RolesAuthorizationRequirement requirement)
    {
        if (!context.User.Identity!.IsAuthenticated) 
            return;
        
        IEnumerable<string> userRoles = context.User
            .FindAll(CustomClaims.Role)
            .Select(c => c.Value);

        if (userRoles.Any(role => requirement.AllowedRoles.Contains(role))) 
            context.Succeed(requirement);
    }
}