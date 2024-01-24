using DigiDent.API.Extensions;
using DigiDent.Application.ClinicCore.ProvidedServices.Commands.AddService;
using DigiDent.Application.ClinicCore.ProvidedServices.Commands.UpdateService;
using DigiDent.Application.ClinicCore.ProvidedServices.Queries.GetAllProvidedServices;
using DigiDent.Application.ClinicCore.ProvidedServices.Queries.GetProvidedServiceById;
using DigiDent.Domain.SharedKernel.ReturnTypes;
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
        GetAllProvidedServicesQuery query = new();
        
        var providedServices = await sender.Send(
            query, cancellationToken);

        return Results.Ok(providedServices);
    }
    
    private static async Task<IResult> GetSpecificProvidedServiceInfo(
        [FromRoute]Guid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        GetProvidedServiceByIdQuery query = new(id);
        
        SpecificProvidedServiceDTO? providedService = await sender.Send(
            query, cancellationToken);

        return providedService is null
            ? Results.NotFound()
            : Results.Ok(providedService);
    }
    
    private static async Task<IResult> AddProvidedService(
        [FromBody]AddProvidedServiceRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        Result<AddProvidedServiceCommand> commandResult = AddProvidedServiceCommand
            .CreateFromRequest(request);
            
        if (commandResult.IsFailure)
            return commandResult.MapToIResult();
            
        Result<Guid> additionResult = await sender.Send(
            commandResult.Value!, cancellationToken);

        return additionResult.Match(
            onFailure: _ => additionResult.MapToIResult(),
            onSuccess: id => Results.Created(
                $"/services/{id}", id));
    }
    
    private static async Task<IResult> UpdateProvidedService(
        [FromRoute]Guid id,
        [FromBody]UpdateProvidedServiceRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        Result<UpdateProvidedServiceCommand> commandResult = UpdateProvidedServiceCommand
            .CreateFromRequest(id, request);

        if (commandResult.IsFailure)
            return commandResult.MapToIResult();
        
        Result updateResult = await sender.Send(
            commandResult.Value!, cancellationToken);

        return updateResult.Match(
            onFailure: _ => updateResult.MapToIResult(),
            onSuccess: () => Results.NoContent());
    }
}