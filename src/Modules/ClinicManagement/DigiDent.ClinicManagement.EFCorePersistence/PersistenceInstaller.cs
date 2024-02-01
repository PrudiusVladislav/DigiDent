using DigiDent.ClinicManagement.Domain;
using DigiDent.ClinicManagement.Domain.Visits.Abstractions;
using DigiDent.ClinicManagement.EFCorePersistence.Constants;
using DigiDent.ClinicManagement.EFCorePersistence.Visits.Repositories;
using DigiDent.Shared.Infrastructure.EfCore.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace DigiDent.ClinicManagement.EFCorePersistence;

public static class PersistenceInstaller
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddSqlServerDbContext<ClinicManagementDbContext>(
                connectionString: configuration.GetConnectionString("SqlServer")!,
                schema: ConfigurationConstants.ClinicManagementSchema)
            .AddRepositories();
        
        return services;
    }
    
    private static IServiceCollection AddRepositories(
        this IServiceCollection services)
    {
        const string repositorySuffix = "Repository";
        
        services.Scan(scan => scan
            .FromAssemblies(
                typeof(DomainInstaller).Assembly,
                typeof(PersistenceInstaller).Assembly)
            .AddClasses(filter => filter
                .Where(c => c.
                    Name.EndsWith(repositorySuffix)))
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsMatchingInterface()
            .WithScopedLifetime());
        
        services.Decorate<IProvidedServicesRepository, CachingProvidedServicesRepository>();

        services.AddMemoryCache();
        
        return services;
    }
}