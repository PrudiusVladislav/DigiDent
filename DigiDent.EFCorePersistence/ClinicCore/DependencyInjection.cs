using DigiDent.Domain.ClinicCoreContext.Employees.Doctors;
using DigiDent.Domain.ClinicCoreContext.Shared.Abstractions;
using DigiDent.EFCorePersistence.ClinicCore.Employees.Doctors;
using DigiDent.EFCorePersistence.ClinicCore.Shared.Repositories;
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

        services.AddTransient<IPersonRepository, PersonRepository>();
        services.AddTransient<IDoctorRepository, DoctorRepository>();
        // services.AddTransient<IAssistantsRepository, AssistantsRepository>();
        // services.AddTransient<IDoctorsRepository, DoctorsRepository>();
        // services.AddTransient<IPatientsRepository, PatientsRepository>();
        // services.AddTransient<IVisitsRepository, VisitsRepository>();
        return services;
    }
}