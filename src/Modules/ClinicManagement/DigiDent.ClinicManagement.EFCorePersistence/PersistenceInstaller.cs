using DigiDent.ClinicManagement.Domain;
using DigiDent.ClinicManagement.Domain.Visits.Abstractions;
using DigiDent.ClinicManagement.EFCorePersistence.Constants;
using DigiDent.ClinicManagement.EFCorePersistence.Visits.Repositories;
using DigiDent.Shared.Infrastructure.Persistence.EfCore.Interceptors;
using DigiDent.Shared.Infrastructure.Persistence.Extensions;
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
            .AddSingleton<PublishDomainEventsInterceptor>()
            .AddSqlServerDbContext<ClinicManagementDbContext>(
                connectionString: configuration.GetConnectionString("SqlServer")!,
                schema: ConfigurationConstants.ClinicManagementSchema)
            .AddRepositories();
        
        return services;
    }
    
    private static IServiceCollection AddRepositories(
        this IServiceCollection services)
    {
        services.AddMatchingRepositories(
            fromAssemblies: 
            [
                typeof(DomainInstaller).Assembly,
                typeof(PersistenceInstaller).Assembly
            ]);
        
        services.Decorate<IProvidedServicesRepository, CachingProvidedServicesRepository>();
        services.AddMemoryCache();
        
        return services;
    }
}