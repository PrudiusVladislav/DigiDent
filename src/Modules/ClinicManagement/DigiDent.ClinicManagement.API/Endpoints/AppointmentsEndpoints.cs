using DigiDent.ClinicManagement.Application.Appointments.Commands.AddAppointmentMediaFiles;
using DigiDent.ClinicManagement.Application.Appointments.Commands.CloseAppointment;
using DigiDent.ClinicManagement.Application.Appointments.Commands.CreateAppointment;
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
        appointmentsGroup.MapPut("/{id}", CloseAppointment);

        appointmentsGroup.MapPut("/{id}/media", AddAppointmentMediaFiles);
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
        [FromForm(Name = "Media")]List<IFormFile> files,
        [FromServices]ISender sender,
        CancellationToken cancellationToken)
    {
        AddAppointmentMediaFilesCommand command = new(id, files);
        
        Result result = await sender.Send(command, cancellationToken);

        return result.Match(onSuccess: () => Results.Ok());
    }
}