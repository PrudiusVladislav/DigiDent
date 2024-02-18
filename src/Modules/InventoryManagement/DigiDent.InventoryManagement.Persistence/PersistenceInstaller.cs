using DigiDent.InventoryManagement.Domain;
using DigiDent.InventoryManagement.Persistence.Constants;
using DigiDent.Shared.Infrastructure.EfCore.Extensions;
using DigiDent.Shared.Infrastructure.EfCore.Interceptors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DigiDent.InventoryManagement.Persistence;

public static class PersistenceInstaller
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddSingleton<PublishDomainEventsInterceptor>()
            .AddSqlServerDbContext<InventoryManagementDbContext>(
                connectionString: configuration.GetConnectionString("SqlServer")!,
                schema: ConfigurationConstants.InventoryManagementSchema)
            .AddMatchingRepositories(
                fromAssemblies: 
                [
                    typeof(DomainInstaller).Assembly,
                    typeof(PersistenceInstaller).Assembly
                ]);
        
        return services;
    }
}