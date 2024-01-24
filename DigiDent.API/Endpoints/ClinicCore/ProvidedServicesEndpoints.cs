using DigiDent.API.Extensions;
using DigiDent.Application.ClinicCore.ProvidedServices.Commands.AddService;
using DigiDent.Application.ClinicCore.ProvidedServices.Commands.UpdateService;
using DigiDent.Application.ClinicCore.ProvidedServices.Queries.GetAllProvidedServices;
using DigiDent.Application.ClinicCore.ProvidedServices.Queries.GetProvidedServiceById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DigiDent.API.Endpoints.ClinicCore;

public static class ProvidedServicesEndpoints
{
    public static RouteGroupBuilder MapProvidedServicesEndpoints(
        this RouteGroupBuilder groupBuilder)
    {
        var servicesGroup = groupBuilder.MapGroup("/services");
        
        servicesGroup.MapGet("/", GetAllProvidedServices);
        servicesGroup.MapGet("/{id}", GetSpecificProvidedServiceInfo);
        servicesGroup.MapPost("/", AddProvidedService);
        servicesGroup.MapPut("/{id}", UpdateProvidedService);
        
        return groupBuilder;
    }
    
    private static async Task<IResult> GetAllProvidedServices(
        ISender sender,
        CancellationToken cancellationToken)
    {
        var providedServices = await sender.Send(
            new GetAllProvidedServicesQuery(), cancellationToken);

        return Results.Ok(providedServices);
    }
    
    private static async Task<IResult> GetSpecificProvidedServiceInfo(
        [FromRoute]Guid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        SpecificProvidedServiceDTO? providedService = await sender.Send(
            new GetProvidedServiceByIdQuery(id), cancellationToken);

        return providedService is null
            ? Results.NotFound()
            : Results.Ok(providedService);
    }
    
    private static async Task<IResult> AddProvidedService(
        [FromBody]AddProvidedServiceRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var commandCreationResult = AddProvidedServiceCommand
            .CreateFromRequest(request);
            
        if (commandCreationResult.IsFailure)
            return commandCreationResult.MapToIResult();
            
        var result = await sender.Send(
            commandCreationResult.Value!, cancellationToken);

        return result.Match(
            onFailure: _ => result.MapToIResult(),
            onSuccess: id => Results.Created(
                $"/services/{id}", id));
    }
    
    private static async Task<IResult> UpdateProvidedService(
        [FromRoute]Guid id,
        [FromBody]UpdateProvidedServiceRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var commandCreationResult = UpdateProvidedServiceCommand
            .CreateFromRequest(id, request);

        if (commandCreationResult.IsFailure)
            return commandCreationResult.MapToIResult();
        
        var result = await sender.Send(
            commandCreationResult.Value!, cancellationToken);

        return result.Match(
            onFailure: _ => result.MapToIResult(),
            onSuccess: () => Results.NoContent());
    }
}