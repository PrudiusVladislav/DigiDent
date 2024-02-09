using Amazon.S3.Model;
using DigiDent.ClinicManagement.Application.Appointments.Commands.AddAppointmentMediaFiles;
using DigiDent.ClinicManagement.Application.Appointments.Commands.CloseAppointment;
using DigiDent.ClinicManagement.Application.Appointments.Commands.CreateAppointment;
using DigiDent.ClinicManagement.Application.Appointments.Queries.GetVisitMediaFiles;
using DigiDent.Shared.Infrastructure.Api;
using DigiDent.Shared.Kernel.Abstractions;
using DigiDent.Shared.Kernel.ReturnTypes;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace DigiDent.ClinicManagement.API.Endpoints;

internal static class AppointmentsEndpoints
{
    internal static IEndpointRouteBuilder MapAppointmentsEndpoints(
        this IEndpointRouteBuilder builder)
    {
        var appointmentsGroup = builder.MapGroup("/appointments");

        appointmentsGroup.MapPost("/", CreateAppointment);
        appointmentsGroup.MapPut("/{id:guid}", CloseAppointment);

        appointmentsGroup.MapPut("/{id:guid}/media", AddAppointmentMediaFiles)
            .DisableAntiforgery();
        
        appointmentsGroup.MapGet("/{id:guid}/media", GetVisitMediaFiles);
        return builder;
    }

    private static async Task<IResult> CreateAppointment(
        [FromBody]CreateAppointmentRequest request,
        [FromServices]ISender sender,
        [FromServices]IDateTimeProvider dateTimeProvider,
        CancellationToken cancellationToken)
    {
        Result<CreateAppointmentCommand> commandResult = CreateAppointmentCommand
            .CreateFromRequest(request, dateTimeProvider);
            
        if (commandResult.IsFailure)
            return commandResult.ProcessFailureResponse();
            
        Result<Guid> creationResult = await sender.Send(
            commandResult.Value!, cancellationToken);

        return creationResult.Match(onSuccess: id => 
            Results.Created($"/appointments/{id}", id));
    }
    
    private static async Task<IResult> CloseAppointment(
        [FromRoute]Guid id,
        [FromBody]CloseAppointmentRequest request,
        [FromServices]ISender sender,
        CancellationToken cancellationToken)
    {
        Result<CloseAppointmentCommand> commandResult = CloseAppointmentCommand
            .CreateFromRequest(id, request);
        
        if (commandResult.IsFailure)
            return commandResult.ProcessFailureResponse();
            
        Result result = await sender.Send(
            commandResult.Value!, cancellationToken);

        return result.Match(onSuccess: () => Results.Ok());
    }
    
    private static async Task<IResult> AddAppointmentMediaFiles(
        [FromRoute]Guid id,
        [FromServices]ISender sender,
        [FromServices]IHttpContextAccessor contextAccessor,
        CancellationToken cancellationToken)
    {
        var files = contextAccessor.HttpContext!.Request
            .Form.Files.ToList();
        
        AddAppointmentMediaFilesCommand command = new(id, files);
        
        Result result = await sender.Send(command, cancellationToken);

        return result.Match(onSuccess: () => Results.Ok());
    }
    
    private static async Task<IResult> GetVisitMediaFiles(
        [FromRoute]Guid id,
        [FromServices]ISender sender,
        CancellationToken cancellationToken)
    {
        GetVisitMediaFilesQuery query = new(id);
        
        GetObjectResponse? response = await sender.Send(query, cancellationToken);
        
        return response is null
            ? Results.NotFound()
            : Results.File(response.ResponseStream, response.Headers.ContentType);
    }
}