using DigiDent.Shared.Abstractions.Modules;
using DigiDent.UserAccess.API.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace DigiDent.UserAccess.API;

/// <summary>
/// Marker and loader class for the UserAccess module
/// </summary>
public sealed class UserAccessModule: IModule
{
    public string Name => nameof(UserAccessModule);
    public void RegisterDependencies(IServiceCollection services)
    {
        throw new NotImplementedException();
    }

    public void RegisterEndpoints(IEndpointRouteBuilder builder)
    {
        builder.MapGroup("/user-access")
            .MapSignUpEndpoints()
            .MapTokensEndpoints();
    }
}