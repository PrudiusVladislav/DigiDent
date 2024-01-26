using System.Text;
using DigiDent.Application.ClinicCore.Abstractions;
using DigiDent.Application.UserAccess.Abstractions;
using DigiDent.Infrastructure.ClinicCore;
using DigiDent.Infrastructure.UserAccess.Authentication;
using DigiDent.Infrastructure.UserAccess.Authorization.RolesRequirement;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using QuestPDF.Infrastructure;

namespace DigiDent.Infrastructure;

public static class InfrastructureAssembly
{
    /// <summary>
    /// Adds and configures infrastructure services.
    /// </summary>
    /// <returns></returns>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        IConfigurationSection configurationSection)
    {
        
        services
            .ConfigureJwt(configurationSection)
            .ConfigureAuthorizationServices(configurationSection)
            .AddAuthorization()
            .AddFactories()
            .ConfigurePDFLicense();
        
        return services;
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
            ValidIssuer = configurationSection["Issuer"],
            ValidateAudience = true,
            ValidAudience = configurationSection["Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configurationSection["Secret"]!)),
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
        services.AddSingleton<IPersonFactory, PersonFactory>();
        return services;
    }
    
    private static IServiceCollection ConfigurePDFLicense(
        this IServiceCollection services)
    {
        QuestPDF.Settings.License = LicenseType.Community;
        return services;
    }
}