using DigiDent.Infrastructure.UserAccess.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace DigiDent.Infrastructure.UserAccess.Authorization.RolesRequirement;

public class RolesAuthorizationHandler: AuthorizationHandler<RolesAuthorizationRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        RolesAuthorizationRequirement requirement)
    {
        if (context.User.Identity!.IsAuthenticated)
        {
            var userRoles = context.User
                .FindAll(CustomClaims.Role)
                .Select(c => c.Value);

            if (userRoles.Any(role => requirement.AllowedRoles.Contains(role))) 
                context.Succeed(requirement);
        }
        return Task.CompletedTask;
    }
}