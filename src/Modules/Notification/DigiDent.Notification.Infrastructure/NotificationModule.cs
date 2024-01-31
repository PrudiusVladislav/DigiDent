using DigiDent.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DigiDent.Notification.Infrastructure;

/// <summary>
/// Marker and loader class for the Notification module
/// </summary>
public sealed class NotificationModule: IModule
{
    public string Name => nameof(NotificationModule);

    public void RegisterDependencies(IServiceCollection services, IConfiguration configuration,
        MediatRServiceConfiguration mediatrConfiguration)
    {
        throw new NotImplementedException();
    }

    public void RegisterEndpoints(IEndpointRouteBuilder builder)
    {
        throw new NotImplementedException();
    }
}