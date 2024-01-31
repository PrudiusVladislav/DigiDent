using DigiDent.Shared.Infrastructure.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace DigiDent.Notification.Infrastructure;

/// <summary>
/// Marker and loader class for the Notification module
/// </summary>
public sealed class NotificationModule: IModule
{
    public string Name => nameof(NotificationModule);
    
    public void Register(IServiceCollection services)
    {
        throw new NotImplementedException();
    }
}