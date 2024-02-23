using System.Reflection;
using DigiDent.Shared.Infrastructure.Persistence.EfCore.Interceptors;
using DigiDent.Shared.Infrastructure.Persistence.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace DigiDent.Shared.Infrastructure.Persistence.Extensions;

public static class InstallerExtensions
{
    /// <summary>
    /// Configures and registers a DbContext. Adds <see cref="PublishDomainEventsInterceptor"/> interceptor to the DbContext.
    /// </summary>
    /// <param name="services"> Service collection. </param>
    /// <param name="connectionString"> Connection string to the database. </param>
    /// <param name="schema"> Schema name. </param>
    /// <typeparam name="TDbContext"> DbContext type. </typeparam>
    /// <returns></returns>
    public static IServiceCollection AddSqlServerDbContext<TDbContext>(
        this IServiceCollection services,
        string connectionString,
        string schema)
        where TDbContext : DbContext
    {
        services.AddDbContext<TDbContext>((sp, options) =>
        {
            options.UseSqlServer(connectionString, 
                builder => builder
                    .MigrationsAssembly(typeof(TDbContext).Assembly.FullName)
                    .MigrationsHistoryTable(
                        tableName: "__EFMigrationsHistory",
                        schema: schema));

            options.AddInterceptors(sp
                .GetRequiredService<PublishDomainEventsInterceptor>());
        });
        
        return services;
    }
    
    /// <summary>
    /// Scans the specified assemblies for classes that end with "Repository" and registers them as their matching interface with scoped lifetime.
    /// </summary>
    /// <param name="services"> Service collection. </param>
    /// <param name="fromAssemblies"> Assemblies to scan. </param>
    /// <returns></returns>
    public static IServiceCollection AddMatchingRepositories(
        this IServiceCollection services,
        params Assembly[] fromAssemblies)
    {
        const string repositorySuffix = "Repository";
        
        services.Scan(scan => scan
            .FromAssemblies(fromAssemblies)
            .AddClasses(filter => filter
                .Where(c => c.
                    Name.EndsWith(repositorySuffix)))
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsMatchingInterface()
            .WithScopedLifetime());
        
        return services;
    }

    public static IServiceCollection AddEfCoreInterceptor<TInterceptor>(
        this IServiceCollection services)
        where TInterceptor: class, IInterceptor
    {
        services.AddSingleton<TInterceptor>();
        
        return services;
    }

    public static IServiceCollection AddSqlConnectionFactory(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddSingleton<SqlConnectionFactory>(provider => 
            new SqlConnectionFactory(connectionString));

        return services;
    }
}