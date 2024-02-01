using Microsoft.Extensions.DependencyInjection;

namespace DigiDent.ClinicManagement.Application;

public static class ApplicationInstaller
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        MediatRServiceConfiguration mediatrConfiguration)
    {
        mediatrConfiguration.RegisterServicesFromAssembly(
            typeof(ApplicationInstaller).Assembly);
        
        services.AddAutoMapper(typeof(ApplicationInstaller).Assembly);
        
        return services;
    }
}