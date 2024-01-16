﻿using DigiDent.Application.UserAccess.Abstractions;
using DigiDent.Domain.UserAccessContext.Users;
using DigiDent.EFCorePersistence.Shared;
using DigiDent.EFCorePersistence.UserAccess.RefreshTokens;
using DigiDent.EFCorePersistence.UserAccess.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DigiDent.EFCorePersistence.UserAccess;

public static class DependencyInjection
{
    /// <summary>
    /// Configures Entity Framework Core persistence for the UserAccess bounded context.
    /// </summary>
    /// <returns></returns>
    public static IServiceCollection AddUserAccessPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<PublishDomainEventsInterceptor>();
        services.AddDbContext<UserAccessDbContext>((sp, options) =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SqlServer"), 
                builder => builder
                    .MigrationsAssembly(typeof(UserAccessDbContext).Assembly.FullName)
                    .MigrationsHistoryTable(
                        tableName: "__EFMigrationsHistory",
                        schema: UserAccessDbContext.UserAccessSchema));
            
            options.AddInterceptors(sp
                .GetRequiredService<PublishDomainEventsInterceptor>());
        });
        
        services.AddTransient<IUsersRepository, UsersRepository>(); 
        services.AddTransient<IRefreshTokensRepository, RefreshTokensRepository>();
        return services;
    }
}