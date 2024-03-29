﻿using DigiDent.UserAccess.Domain.Users.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DigiDent.UserAccess.Domain;

public static class DomainInstaller
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<UsersDomainService>();
        return services;
    }
}