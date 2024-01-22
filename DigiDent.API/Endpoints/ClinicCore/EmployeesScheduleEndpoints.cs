
using DigiDent.API.Extensions;
using DigiDent.Application.ClinicCore.EmployeesSchedule.Commands.AddSchedulePreference;
using DigiDent.Application.ClinicCore.EmployeesSchedule.Commands.AddWorkingDay;
using DigiDent.Application.ClinicCore.EmployeesSchedule.Queries.GetSchedulePreferencesForEmployee;
using DigiDent.Application.ClinicCore.EmployeesSchedule.Queries.GetWorkingDaysForEmployee;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DigiDent.API.Endpoints.ClinicCore;

public static class EmployeesScheduleEndpoints
{
    public static RouteGroupBuilder MapEmployeesScheduleEndpoints(
        this RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapGroup("/employees")
            .MapGetEmployeeScheduleDataEndpoints()
            .MapAddScheduleDataEndpoints();
        
        return groupBuilder;
    }

    private static IEndpointRouteBuilder MapGetEmployeeScheduleDataEndpoints(
        this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:guid}/schedule", async (
            [FromRoute]Guid id,
            [FromQuery]DateOnly from,
            [FromQuery]DateOnly until,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var query = new GetWorkingDaysForEmployeeQuery(id, from, until);
            
            var workingDays = await mediator.Send(
                query, cancellationToken);
            
            return Results.Ok(workingDays);
            //TODO: implement pdf response
        });
        
        app.MapGet("/{id:guid}/schedule/preferences", async (
            [FromRoute]Guid id,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var query = new GetSchedulePreferencesQuery(id);
            
            var workingDays = await mediator.Send(
                query, cancellationToken);
            
            return Results.Ok(workingDays);
        });
        
        return app;
    }
    
    private static IEndpointRouteBuilder MapAddScheduleDataEndpoints(
        this IEndpointRouteBuilder app)
    {
        app.MapPost("/{id:guid}/schedule", async (
            [FromRoute]Guid id,
            [FromBody]AddWorkingDayRequest request,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var commandCreationResult = AddWorkingDayCommand
                .CreateFromRequest(id, request);
            
            if (commandCreationResult.IsFailure)
                return commandCreationResult.MapToIResult();
            
            var result = await mediator.Send(
                commandCreationResult.Value!, cancellationToken);

            return result.Match(
                onFailure: _ => result.MapToIResult(),
                onSuccess: () => Results.NoContent());
        });
        
        app.MapPost("/{id:guid}/schedule/preferences", async (
            [FromRoute]Guid id,
            [FromBody]AddSchedulePreferenceRequest request,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var commandCreationResult = AddSchedulePreferenceCommand
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