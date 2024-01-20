using DigiDent.Application.ClinicCore.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace DigiDent.Infrastructure.ClinicCore;

public static class DependencyInjection
{
    public static IServiceCollection AddClinicCoreInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IPersonFactory, PersonFactory>();
        
        return services;
    }
}