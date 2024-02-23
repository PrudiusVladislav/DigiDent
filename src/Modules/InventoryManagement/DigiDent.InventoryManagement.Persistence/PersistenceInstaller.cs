using DigiDent.InventoryManagement.Domain;
using DigiDent.InventoryManagement.Persistence.Constants;
using DigiDent.InventoryManagement.Persistence.Shared;
using DigiDent.Shared.Infrastructure.Persistence.EfCore.Interceptors;
using DigiDent.Shared.Infrastructure.Persistence.Extensions;
using DigiDent.Shared.Infrastructure.Persistence.Factories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DigiDent.InventoryManagement.Persistence;

public static class PersistenceInstaller
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SqlServer")!;
        
        services
            .AddEfCoreInterceptor<PublishDomainEventsInterceptor>()
            
            .AddSqlServerDbContext<InventoryManagementDbContext>(
                connectionString,
                schema: ConfigurationConstants.InventoryManagementSchema)
            
            .AddSqlConnectionFactory(connectionString)
            
            .AddMatchingRepositories(
                fromAssemblies: 
                [
                    typeof(DomainInstaller).Assembly,
                    typeof(PersistenceInstaller).Assembly
                ]);
        
        return services;
    }
}