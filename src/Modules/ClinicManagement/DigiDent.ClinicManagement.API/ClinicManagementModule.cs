using DigiDent.Shared.Infrastructure.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace DigiDent.ClinicManagement.API;

/// <summary>
/// Marker and loader class for the ClinicManagement module
/// </summary>
public sealed class ClinicManagementModule: IModule
{
    public string Name => nameof(ClinicManagementModule);
    
    public void Register(IServiceCollection services)
    {
        throw new NotImplementedException();
    }
}