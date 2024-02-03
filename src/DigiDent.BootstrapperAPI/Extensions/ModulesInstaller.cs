using DigiDent.ClinicManagement.API;
using DigiDent.Notification.Infrastructure;
using DigiDent.Shared.Abstractions.Modules;
using DigiDent.UserAccess.API;

namespace DigiDent.BootstrapperAPI.Extensions;

public static class ModulesInstaller
{
    private static IReadOnlyList<IModule> _modulesToInstall = 
    [
        new ClinicManagementModule(),
        new NotificationModule(),
        new UserAccessModule()
    ];
    
    public static IServiceCollection AddModulesDependencies(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        MediatRServiceConfiguration mediatrConfiguration = new();

        foreach (var module in _modulesToInstall)
        {
            module.RegisterDependencies(
                services, configuration, mediatrConfiguration);
        }

        services.AddMediatR(mediatrConfiguration);
        
        return services;
    }
    
    public static IEndpointRouteBuilder MapModulesEndpoints(
        this IEndpointRouteBuilder endpointsBuilder)
    {
        foreach (var module in _modulesToInstall)
        {
            module.RegisterEndpoints(
                builder: endpointsBuilder, 
                baseApiUri: new Uri("https://localhost:7102/"));
        }

        return endpointsBuilder;
    }
}