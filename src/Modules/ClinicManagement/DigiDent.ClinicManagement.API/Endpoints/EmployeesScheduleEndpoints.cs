using DigiDent.ClinicManagement.Application.EmployeesSchedule.Commands.AddSchedulePreference;
using DigiDent.ClinicManagement.Application.EmployeesSchedule.Commands.AddWorkingDay;
using DigiDent.ClinicManagement.Application.EmployeesSchedule.Queries.GetSchedulePreferencesForEmployee;
using DigiDent.ClinicManagement.Application.EmployeesSchedule.Queries.GetWorkingDaysForEmployee;
using DigiDent.ClinicManagement.Infrastructure.EmployeeSchedulePDFDoc;
using DigiDent.Shared.Infrastructure.Api;
using DigiDent.Shared.Kernel.ReturnTypes;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using QuestPDF.Fluent;

namespace DigiDent.ClinicManagement.API.Endpoints;

public static class EmployeesScheduleEndpoints
{
    public static RouteGroupBuilder MapEmployeesScheduleEndpoints(
        this RouteGroupBuilder groupBuilder)
    {
        var employeesGroup = groupBuilder.MapGroup("/employees");
        
        employeesGroup.MapGet("/{id}/schedule", GetEmployeeWorkingSchedule);
        employeesGroup.MapGet("/{id}/preferences", GetEmployeeSchedulePreferences);
        employeesGroup.MapPost("/{id}/schedule", AddEmployeeWorkingDay);
        employeesGroup.MapPost("/{id}/preferences", AddEmployeeSchedulePreference);
        
        return groupBuilder;
    }
    
    private static async Task<IResult> GetEmployeeWorkingSchedule(
        [FromRoute]Guid id,
        [FromQuery]DateOnly from,
        [FromQuery]DateOnly until,
        ISender sender,
        CancellationToken cancellationToken,
        [FromQuery]bool pdf = false)
    {
        GetWorkingDaysForEmployeeQuery query = new(id, from, until);
            
        var workingDays = await sender.Send(
            query, cancellationToken);
            
        if (pdf is false)
            return Results.Ok(workingDays);
            
        ScheduleDocumentDataModel documentDataModel = new(
            "DigiDent",
            workingDays.First().EmployeeFullName,
            from,
            until,
            workingDays.ToList());

        byte[] document = new EmployeeScheduleDocument(documentDataModel)
            .GeneratePdf();
            
        return Results.File(document, "application/pdf", "schedule.pdf");
    }
    
    private static async Task<IResult> GetEmployeeSchedulePreferences(
        [FromRoute]Guid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        GetSchedulePreferencesQuery query = new(id);
            
        var preferences = await sender.Send(
            query, cancellationToken);
            
        return Results.Ok(preferences);
    }
    
    private static async Task<IResult> AddEmployeeWorkingDay(
        [FromRoute]Guid id,
        [FromBody]AddWorkingDayRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        Result<AddWorkingDayCommand> commandResult = AddWorkingDayCommand
            .CreateFromRequest(id, request);
            
        if (commandResult.IsFailure)
            return commandResult.MapToIResult();
            
        Result additionResult = await sender.Send(
            commandResult.Value!, cancellationToken);

        return additionResult.Match(
            onFailure: _ => additionResult.MapToIResult(),
            onSuccess: () => Results.NoContent());
    }
    
    private static async Task<IResult> AddEmployeeSchedulePreference(
        [FromRoute]Guid id,
        [FromBody]AddSchedulePreferenceRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        Result<AddSchedulePreferenceCommand> commandResult = AddSchedulePreferenceCommand
            .CreateFromRequest(request, employeeId: id);
            
        if (commandResult.IsFailure)
            return commandResult.MapToIResult();
            
        Result additionResult = await sender.Send(
            commandResult.Value!, cancellationToken);

        return additionResult.Match(
            onFailure: _ => additionResult.MapToIResult(),
            onSuccess: () => Results.NoContent());
    }
}