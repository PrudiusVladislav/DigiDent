using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
    
namespace DigiDent.Shared.Abstractions.Modules;

public interface IModule
{
    string Name { get; }
    
    void RegisterDependencies(
        IServiceCollection services,
        IConfiguration configuration,
        MediatRServiceConfiguration mediatrConfiguration);
    
    void RegisterEndpoints(IEndpointRouteBuilder builder);
}