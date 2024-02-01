using DigiDent.Shared.Abstractions.Modules;
using DigiDent.UserAccess.API.Endpoints;
using DigiDent.UserAccess.Application;
using DigiDent.UserAccess.Domain;
using DigiDent.UserAccess.EFCorePersistence;
using DigiDent.UserAccess.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DigiDent.UserAccess.API;

/// <summary>
/// Marker and loader class for the UserAccess module
/// </summary>
public sealed class UserAccessModule: IModule
{
    public string Name => nameof(UserAccessModule);
    
    public void RegisterDependencies(
        IServiceCollection services,
        IConfiguration configuration,
        MediatRServiceConfiguration mediatrConfiguration)
    {
        services
            .AddDomain()
            .AddPersistence(configuration)
            .AddApplication(mediatrConfiguration)
            .AddInfrastructure(configuration.GetSection("Authentication:Jwt"));
    }

    public void RegisterEndpoints(IEndpointRouteBuilder builder)
    {
        builder.MapGroup("/user-access")
            .MapSignUpEndpoints()
            .MapTokensEndpoints();
    }
}