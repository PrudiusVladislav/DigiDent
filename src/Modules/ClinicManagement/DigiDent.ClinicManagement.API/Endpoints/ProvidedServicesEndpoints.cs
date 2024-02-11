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

internal static class ProvidedServicesEndpoints
{
    internal static IEndpointRouteBuilder MapProvidedServicesEndpoints(
        this IEndpointRouteBuilder builder)
    {
        var servicesGroup = builder.MapGroup("/services");
        
        servicesGroup.MapGet("/", GetAllProvidedServices);
        servicesGroup.MapGet("/{id}", GetSpecificProvidedServiceInfo);
        servicesGroup.MapPost("/", AddProvidedService);
        servicesGroup.MapPut("/{id}", UpdateProvidedService);
        
        return builder;
    }
    
    private static async Task<IResult> GetAllProvidedServices(
        [FromServices]ISender sender,
        CancellationToken cancellationToken)
    {
        GetAllProvidedServicesQuery query = new();
        
        var providedServices = await sender.Send(
            query, cancellationToken);

        return Results.Ok(providedServices);
    }
    
    private static async Task<IResult> GetSpecificProvidedServiceInfo(
        [FromRoute]Guid id,
        [FromServices]ISender sender,
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
        [FromServices]ISender sender,
        CancellationToken cancellationToken)
    {
        Result<AddProvidedServiceCommand> commandResult = AddProvidedServiceCommand
            .CreateFromRequest(request);
            
        if (commandResult.IsFailure)
            return commandResult.ProcessFailureResponse();
            
        Result<Guid> additionResult = await sender.Send(
            commandResult.Value!, cancellationToken);

        return additionResult.Match(
            onFailure: _ => additionResult.ProcessFailureResponse(),
            onSuccess: id => Results.Created(
                $"/services/{id}", id));
    }
    
    private static async Task<IResult> UpdateProvidedService(
        [FromRoute]Guid id,
        [FromBody]UpdateProvidedServiceRequest request,
        [FromServices]ISender sender,
        CancellationToken cancellationToken)
    {
        Result<UpdateProvidedServiceCommand> commandResult = UpdateProvidedServiceCommand
            .CreateFromRequest(request, serviceToUpdateId: id);

        if (commandResult.IsFailure)
            return commandResult.ProcessFailureResponse();
        
        Result updateResult = await sender.Send(
            commandResult.Value!, cancellationToken);

        return updateResult.Match(
            onFailure: _ => updateResult.ProcessFailureResponse(),
            onSuccess: () => Results.NoContent());
    }
}