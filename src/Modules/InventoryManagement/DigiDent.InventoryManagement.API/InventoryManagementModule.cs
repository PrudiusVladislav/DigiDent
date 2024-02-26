using DigiDent.InventoryManagement.API.Endpoints;
using DigiDent.InventoryManagement.Application;
using DigiDent.InventoryManagement.Domain;
using DigiDent.InventoryManagement.Persistence;
using DigiDent.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DigiDent.InventoryManagement.API;

public class InventoryManagementModule: IModule
{
    public string Name => nameof(InventoryManagementModule);

    public void RegisterDependencies(
        IServiceCollection services,
        IConfiguration configuration,
        MediatRServiceConfiguration mediatrConfiguration)
    {
        services
            .AddDomain()
            .AddApplication(mediatrConfiguration)
            .AddPersistence(configuration);
    }

    public void RegisterEndpoints(IEndpointRouteBuilder builder, Uri baseApiUri)
    {
        builder
            .MapGroup("/inventory")
            .MapInventoryItemsEndpoints()
            .MapInventoryActionsEndpoints()
            .MapRequestsEndpoints()
            .MapEmployeeEndpoints();
    }
}