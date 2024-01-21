using DigiDent.API.Extensions;
using DigiDent.Application.ClinicCore.ProvidedServices.Commands.AddService;
using DigiDent.Application.ClinicCore.ProvidedServices.Commands.UpdateService;
using DigiDent.Application.ClinicCore.ProvidedServices.Queries.GetAllProvidedServices;
using DigiDent.Application.ClinicCore.ProvidedServices.Queries.GetProvidedServiceById;
using MediatR;

namespace DigiDent.API.Endpoints.ClinicCore;

public static class ProvidedServicesEndpoints
{
    public static RouteGroupBuilder MapProvidedServices(
        this RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapGroup("/services")
            .MapGetProvidedServicesEndpoints()
            .MapAddProvidedServiceEndpoint();
        
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

        app.MapGet("/{id:guid}", async (
            Guid id,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var providedService = await mediator.Send(
                new GetProvidedServiceByIdQuery(id), cancellationToken);
            
            return providedService is null
                ? Results.NotFound()
                : Results.Ok(providedService);
        });
        
        return app;
    }
    
    private static IEndpointRouteBuilder MapAddProvidedServiceEndpoint(
        this IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (
            AddProvidedServiceRequest request,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var commandCreationResult = AddProvidedServiceCommand
                .CreateFromRequest(request);
            
            if (commandCreationResult.IsFailure)
                return commandCreationResult.MapToIResult();
            
            var result = await mediator.Send(
                commandCreationResult.Value!, cancellationToken);
            
            return result.Match(
                onFailure: _ => result.MapToIResult(),
                onSuccess: id => Results.Created(
                    $"/services/{id}", id));
        });
        
        return app;
    }
    
    private static IEndpointRouteBuilder MapUpdateProvidedServiceEndpoint(
        this IEndpointRouteBuilder app)
    {
        app.MapPut("/{id:guid}", async (
            Guid id,
            UpdateProvidedServiceRequest request,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var commandCreationResult = UpdateProvidedServiceCommand
                .CreateFromRequest(id, request);
            
            if (commandCreationResult.IsFailure)
                return commandCreationResult.MapToIResult();
            
            var result = await mediator.Send(
                commandCreationResult.Value!, cancellationToken);
            
            return result.Match(
                onFailure: _ => result.MapToIResult(),
                onSuccess: () => Results.NoContent());
        });
        
        return app;
    }
}