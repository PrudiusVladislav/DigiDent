﻿using System.Text;
using DigiDent.Application.UserAccess.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace DigiDent.Infrastructure.UserAccess;

public static class DependencyInjection
{
    /// <summary>
    /// Adds and configures infrastructure of the UserAccess bounded context.
    /// </summary>
    /// <returns></returns>
    public static IServiceCollection AddUserAccessInfrastructure(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services
            .ConfigureJwt(configuration)
            .ConfigureAuthorizationServices(configuration);
        return services;
    }
    
    private static IServiceCollection ConfigureJwt(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection("Authentication:Jwt"));
        services.AddTransient<IJwtProvider, JwtProvider>();
        return services;
    }
    
    private static IServiceCollection ConfigureAuthorizationServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IAuthorizationHandler, RolesAuthorizationHandler>();
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!))
                };
            });
        return services;
        //services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
    }
}