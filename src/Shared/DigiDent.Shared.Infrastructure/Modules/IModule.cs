using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace DigiDent.Shared.Infrastructure.Modules;

public interface IModule
{
    string Name { get; }
    void RegisterDependencies(IServiceCollection services);
    void RegisterEndpoints(IEndpointRouteBuilder builder);
}