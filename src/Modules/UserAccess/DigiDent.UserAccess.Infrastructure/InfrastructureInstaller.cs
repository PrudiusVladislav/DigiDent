using System.Text;
using DigiDent.UserAccess.Application.Abstractions;
using DigiDent.UserAccess.Infrastructure.Authentication;
using DigiDent.UserAccess.Infrastructure.Authorization;
using DigiDent.UserAccess.Infrastructure.Authorization.RolesRequirement;
using DigiDent.UserAccess.Infrastructure.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace DigiDent.UserAccess.Infrastructure;

public static class InfrastructureInstaller
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfigurationSection configurationSection)
    {
        return services
            .ConfigureJwt(configurationSection)
            .ConfigureAuthorizationServices(configurationSection)
            .AddAuthorization()
            .AddFactories();
    }
    
    private static IServiceCollection ConfigureJwt(
        this IServiceCollection services,
        IConfigurationSection configurationSection)
    {
        services.Configure<JwtOptions>(configurationSection);
        services.AddTransient<IJwtService, JwtService>();
        return services;
    }
    
    private static IServiceCollection ConfigureAuthorizationServices(
        this IServiceCollection services,
        IConfigurationSection configurationSection)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = configurationSection[AuthenticationConstants.IssuerSectionName],
            ValidateAudience = true,
            ValidAudience = configurationSection[AuthenticationConstants.AudienceSectionName],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    configurationSection[AuthenticationConstants.SecretSectionName]!)),
            ValidateLifetime = true,
            LifetimeValidator = (
                notBefore, expires, securityToken, validationParameters) =>
            {
                if (expires != null)
                {
                    return expires > DateTime.UtcNow;
                }
                return false;
            }
        };
        
        services.AddSingleton(tokenValidationParameters);
        services.AddSingleton<IAuthorizationHandler, RolesAuthorizationHandler>();
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParameters;
            });
        return services;
    }
    
    private static IServiceCollection AddFactories(
        this IServiceCollection services)
    {
        services.AddSingleton<IRoleFactory, RoleFactory>();
        return services;
    }
}