using DigiDent.Application;
using DigiDent.Domain;
using DigiDent.Domain.ClinicCoreContext.Visits.Abstractions;
using DigiDent.Domain.UserAccessContext.Users;
using DigiDent.EFCorePersistence.ClinicCore;
using DigiDent.EFCorePersistence.ClinicCore.Visits.Repositories;
using DigiDent.EFCorePersistence.Shared;
using DigiDent.EFCorePersistence.UserAccess;
using DigiDent.EFCorePersistence.UserAccess.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace DigiDent.EFCorePersistence;

public static class EFCorePersistenceAssembly
{
    /// <summary>
    /// Adds and configures EF Core persistence services.
    /// </summary>
    public static IServiceCollection AddEFCorePersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddSingleton<PublishDomainEventsInterceptor>()
            .AddConfiguredDbContext<UserAccessDbContext>(
                configuration, UserAccessDbContext.UserAccessSchema)
            .AddConfiguredDbContext<ClinicCoreDbContext>(
                configuration, ClinicCoreDbContext.ClinicCoreSchema)
            .RegisterRepositories()
            .RegisterUnitOfWork();
        
        return services;
    }
    
    private static IServiceCollection AddConfiguredDbContext<TDbContext>(
        this IServiceCollection services,
        IConfiguration configuration,
        string schema)
        where TDbContext : DbContext
    {
        services.AddDbContext<TDbContext>((sp, options) =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SqlServer"), 
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

    private static IServiceCollection RegisterRepositories(
        this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblies(
                typeof(DomainAssembly).Assembly,
                typeof(ApplicationAssembly).Assembly,
                typeof(EFCorePersistenceAssembly).Assembly)
            .AddClasses(filter => filter
                .Where(x => x.Name.EndsWith("Repository")))
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsMatchingInterface()
            .WithScopedLifetime());
        
        services.Decorate<IProvidedServicesRepository, CachingProvidedServicesRepository>();

        services.AddMemoryCache();
        
        return services;
    }
    
    private static IServiceCollection RegisterUnitOfWork(
        this IServiceCollection services)
    {
        services.AddScoped<IUsersUnitOfWork, UsersUnitOfWork>();
        return services;
    }
}