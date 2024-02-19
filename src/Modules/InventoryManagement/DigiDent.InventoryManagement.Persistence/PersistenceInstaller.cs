using DigiDent.InventoryManagement.Domain;
using DigiDent.InventoryManagement.Persistence.Constants;
using DigiDent.InventoryManagement.Persistence.Shared;
using DigiDent.Shared.Abstractions.Factories;
using DigiDent.Shared.Infrastructure.EfCore.Extensions;
using DigiDent.Shared.Infrastructure.EfCore.Interceptors;
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
        services
            .AddSingleton<PublishDomainEventsInterceptor>()
            
            .AddSqlServerDbContext<InventoryManagementDbContext>(
                connectionString: configuration.GetConnectionString("SqlServer")!,
                schema: ConfigurationConstants.InventoryManagementSchema)
            
            .AddSingleton<IDbConnectionFactory<SqlConnection>, SqlConnectionFactory>()
            
            .AddMatchingRepositories(
                fromAssemblies: 
                [
                    typeof(DomainInstaller).Assembly,
                    typeof(PersistenceInstaller).Assembly
                ]);
        
        return services;
    }
}