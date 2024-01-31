﻿using DigiDent.Shared.Infrastructure.EfCore.Extensions;
using DigiDent.UserAccess.Application.Abstractions;
using DigiDent.UserAccess.Domain.Users.Abstractions;
using DigiDent.UserAccess.EFCorePersistence.Constants;
using DigiDent.UserAccess.EFCorePersistence.RefreshTokens;
using DigiDent.UserAccess.EFCorePersistence.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DigiDent.UserAccess.EFCorePersistence;

public static class PersistenceInstaller
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddRepositories()
            .AddUnitOfWork()
            .AddSqlServerDbContext<UserAccessDbContext>(
                configuration.GetConnectionString("SqlServer")!,
                ConfigurationConstants.UserAccessSchemaName);
        
        return services;
    }
    
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IRefreshTokensRepository, RefreshTokensRepository>();
        return services;
    }
    
    private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUsersUnitOfWork, UsersUnitOfWork>();
        return services;
    }
}