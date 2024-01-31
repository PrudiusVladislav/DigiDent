using Microsoft.Extensions.DependencyInjection;

namespace DigiDent.Shared.Infrastructure.Modules;

public interface IModule
{
    string Name { get; }
    void Register(IServiceCollection services);
}