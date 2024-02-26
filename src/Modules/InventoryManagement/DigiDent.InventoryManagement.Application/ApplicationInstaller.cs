using Microsoft.Extensions.DependencyInjection;

namespace DigiDent.InventoryManagement.Application;

public static class ApplicationInstaller
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        MediatRServiceConfiguration mediatrConfiguration)
    {
        mediatrConfiguration.RegisterServicesFromAssembly(
            typeof(ApplicationInstaller).Assembly);
        
        return services;
    }
}