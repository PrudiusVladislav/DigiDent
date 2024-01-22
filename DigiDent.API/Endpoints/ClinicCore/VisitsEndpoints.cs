using DigiDent.API.Extensions;
using DigiDent.Application.ClinicCore.Appointments.Commands.CreateAppointment;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DigiDent.API.Endpoints.ClinicCore;

public static class VisitsEndpoints
{
    public static RouteGroupBuilder MapVisitsEndpoints(
        this RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapGroup("/appointments");
        groupBuilder.MapGroup("/past-visits");
        return groupBuilder;
    }
    
    private static IEndpointRouteBuilder MapCreateAppointmentEndpoint(
        this IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (
            [FromBody]CreateAppointmentRequest request,
            IMediator mediator,
            CancellationToken cancellationToken) =>
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
        });
        
        return app;
    }
}