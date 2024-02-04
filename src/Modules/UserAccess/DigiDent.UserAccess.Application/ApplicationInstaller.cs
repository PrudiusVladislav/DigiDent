using Microsoft.Extensions.DependencyInjection;
using Rebus.Config;

namespace DigiDent.UserAccess.Application;

public static class ApplicationInstaller
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        MediatRServiceConfiguration mediatrConfiguration)
    {
        mediatrConfiguration.RegisterServicesFromAssembly(
            typeof(ApplicationInstaller).Assembly);
        
        services.AutoRegisterHandlersFromAssembly(
            typeof(ApplicationInstaller).Assembly);
        
        return services;
    }
}