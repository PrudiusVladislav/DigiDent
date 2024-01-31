using DigiDent.ClinicManagement.Application.ProvidedServices.Commands.AddService;
using DigiDent.ClinicManagement.Application.ProvidedServices.Commands.UpdateService;
using DigiDent.ClinicManagement.Application.ProvidedServices.Queries.GetAllProvidedServices;
using DigiDent.ClinicManagement.Application.ProvidedServices.Queries.GetProvidedServiceById;
using DigiDent.Shared.Infrastructure.Api;
using DigiDent.Shared.Kernel.ReturnTypes;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace DigiDent.ClinicManagement.API.Endpoints;

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
            .CreateFromRequest(request, serviceToUpdateId: id);

        if (commandResult.IsFailure)
            return commandResult.MapToIResult();
        
        Result updateResult = await sender.Send(
            commandResult.Value!, cancellationToken);

        return updateResult.Match(
            onFailure: _ => updateResult.MapToIResult(),
            onSuccess: () => Results.NoContent());
    }
}