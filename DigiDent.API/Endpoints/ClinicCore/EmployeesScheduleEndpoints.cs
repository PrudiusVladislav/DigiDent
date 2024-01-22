
using DigiDent.API.Extensions;
using DigiDent.Application.ClinicCore.EmployeesSchedule.Commands.AddWorkingDay;
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
            .MapGetEmployeeScheduleEndpoint();
        
        return groupBuilder;
    }

    private static IEndpointRouteBuilder MapGetEmployeeScheduleEndpoint(
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
            //TODO: create a query to fetch the working days of an employee
            //possibly implement pdf response
        });
        
        return app;
    }
    
    private static IEndpointRouteBuilder MapAddWorkingDayEndpoint(
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
        
        return app;
    }
}