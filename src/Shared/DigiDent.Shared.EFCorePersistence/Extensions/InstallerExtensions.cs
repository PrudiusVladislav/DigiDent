using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DigiDent.Shared.EFCorePersistence.Extensions;

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
}