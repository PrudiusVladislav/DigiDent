using DigiDent.Application.UserAccess;
using DigiDent.Domain.UserAccessContext.Users.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DigiDent.Infrastructure.UserAccess;

public static class DependencyInjection
{
    public static IServiceCollection AddUserAccessInfrastructure(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.ConfigureJwt(configuration);
        services.ConfigureAuthorizationServices();
        services.ConfigureAuthorizationPolicies();
        return services;
    }
    
    private static void ConfigureJwt(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection("Authentication:Jwt"));
        services.AddTransient<IJwtProvider, JwtProvider>();
    }
    
    private static void ConfigureAuthorizationServices(
        this IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationHandler, RolesAuthorizationHandler>();
        //services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
    }
    
    private static void ConfigureAuthorizationPolicies(
        this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            foreach (var role in Enum.GetValues<Role>())
            {
                options.AddPolicy(role.ToString(), policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole(role.ToString());
                });
            }
        });
    }
}