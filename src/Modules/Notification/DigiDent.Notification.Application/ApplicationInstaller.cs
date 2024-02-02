using Microsoft.Extensions.DependencyInjection;
using Rebus.Config;

namespace DigiDent.Notification.Application;

public static class ApplicationInstaller
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AutoRegisterHandlersFromAssembly(
            typeof(ApplicationInstaller).Assembly);
        
        return services;
    }
}