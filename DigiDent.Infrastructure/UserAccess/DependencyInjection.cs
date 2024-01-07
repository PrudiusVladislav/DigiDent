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
}