using DigiDent.EFCorePersistence.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DigiDent.EFCorePersistence.ClinicCore;

public static class DependencyInjection
{
    public static IServiceCollection AddClinicCorePersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ClinicCoreDbContext>((sp, options) =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SqlServer"), 
                builder => builder
                    .MigrationsAssembly(typeof(ClinicCoreDbContext).Assembly.FullName)
                    .MigrationsHistoryTable(
                        tableName: "__EFMigrationsHistory",
                        schema: ClinicCoreDbContext.ClinicCoreSchema));
            
            options.AddInterceptors(sp
                .GetRequiredService<PublishDomainEventsInterceptor>());
        });
        
        // services.AddTransient<IAssistantsRepository, AssistantsRepository>();
        // services.AddTransient<IDoctorsRepository, DoctorsRepository>();
        // services.AddTransient<IPatientsRepository, PatientsRepository>();
        // services.AddTransient<IVisitsRepository, VisitsRepository>();
        return services;
    }
}