using DigiDent.API.Extensions;
using DigiDent.Application.ClinicCore.Appointments.Commands.CloseAppointment;
using DigiDent.Application.ClinicCore.Appointments.Commands.CreateAppointment;
using DigiDent.Shared.Domain.Abstractions;
using DigiDent.Shared.Domain.ReturnTypes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DigiDent.API.Endpoints.ClinicCore;

public static class AppointmentsEndpoints
{
    public static RouteGroupBuilder MapAppointmentsEndpoints(
        this RouteGroupBuilder groupBuilder)
    {
        var appointmentsGroup = groupBuilder.MapGroup("/appointments");

        appointmentsGroup.MapPost("/", CreateAppointment);
        appointmentsGroup.MapPut("/{id}", CloseAppointment);
           
        return groupBuilder;
    }

    private static async Task<IResult> CreateAppointment(
        [FromBody]CreateAppointmentRequest request,
        ISender sender,
        IDateTimeProvider dateTimeProvider,
        CancellationToken cancellationToken)
    {
        Result<CreateAppointmentCommand> commandResult = CreateAppointmentCommand
            .CreateFromRequest(request, dateTimeProvider);
            
        if (commandResult.IsFailure)
            return commandResult.MapToIResult();
            
        Result<Guid> creationResult = await sender.Send(
            commandResult.Value!, cancellationToken);

        return creationResult.Match(
            onFailure: _ => creationResult.MapToIResult(),
            onSuccess: id => Results.Created(
                $"/appointments/{id}", id));
    }
    
    private static async Task<IResult> CloseAppointment(
        [FromRoute]Guid id,
        [FromBody]CloseAppointmentRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        Result<CloseAppointmentCommand> commandResult = CloseAppointmentCommand
            .CreateFromRequest(id, request);
        
        if (commandResult.IsFailure)
            return commandResult.MapToIResult();
            
        Result result = await sender.Send(
            commandResult.Value!, cancellationToken);

        return result.Match(
            onFailure: _ => result.MapToIResult(),
            onSuccess: () => Results.Ok());
    }
}