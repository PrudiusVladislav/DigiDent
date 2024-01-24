using DigiDent.API.Extensions;
using DigiDent.Application.ClinicCore.Appointments.Commands.CloseAppointment;
using DigiDent.Application.ClinicCore.Appointments.Commands.CreateAppointment;
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
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var commandCreationResult = CreateAppointmentCommand
            .CreateFromRequest(request);
            
        if (commandCreationResult.IsFailure)
            return commandCreationResult.MapToIResult();
            
        var result = await mediator.Send(
            commandCreationResult.Value!, cancellationToken);

        return result.Match(
            onFailure: _ => result.MapToIResult(),
            onSuccess: id => Results.Created(
                $"/appointments/{id}", id));
    }
    
    private static async Task<IResult> CloseAppointment(
        [FromRoute]Guid id,
        [FromBody]CloseAppointmentRequest request,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var commandResult = CloseAppointmentCommand.CreateFromRequest(
            id, request);
        if (commandResult.IsFailure)
            return commandResult.MapToIResult();
            
        var result = await mediator.Send(
            commandResult.Value!, cancellationToken);

        return result.Match(
            onFailure: _ => result.MapToIResult(),
            onSuccess: () => Results.Ok());
    }
}