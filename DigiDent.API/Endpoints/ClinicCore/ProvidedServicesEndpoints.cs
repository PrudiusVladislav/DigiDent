using DigiDent.Application.ClinicCore.ProvidedServices.Queries.GetAllProvidedServices;
using MediatR;

namespace DigiDent.API.Endpoints.ClinicCore;

public static class ProvidedServicesEndpoints
{
    public static RouteGroupBuilder MapProvidedServices(
        this RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapGroup("/services")
            .MapGetProvidedServicesEndpoints();
        
        return groupBuilder;
    }
    
    private static IEndpointRouteBuilder MapGetProvidedServicesEndpoints(
        this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var providedServices = await mediator.Send(
                new GetAllProvidedServicesQuery(), cancellationToken);
            return Results.Ok(providedServices);
        });
        
        return app;
    }
}