using DigiDent.API.Extensions;
using DigiDent.Application.ClinicCore.EmployeesSchedule.Commands.AddSchedulePreference;
using DigiDent.Application.ClinicCore.EmployeesSchedule.Commands.AddWorkingDay;
using DigiDent.Application.ClinicCore.EmployeesSchedule.Queries.GetSchedulePreferencesForEmployee;
using DigiDent.Application.ClinicCore.EmployeesSchedule.Queries.GetWorkingDaysForEmployee;
using DigiDent.Infrastructure.ClinicCore.EmployeeSchedulePDFDoc;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;

namespace DigiDent.API.Endpoints.ClinicCore;

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
        var query = new GetWorkingDaysForEmployeeQuery(id, from, until);
            
        var workingDays = await sender.Send(
            query, cancellationToken);
            
        if (pdf is false)
            return Results.Ok(workingDays);
            
        var documentDataModel = new ScheduleDocumentDataModel(
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
        var query = new GetSchedulePreferencesQuery(id);
            
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
        var commandCreationResult = AddWorkingDayCommand
            .CreateFromRequest(id, request);
            
        if (commandCreationResult.IsFailure)
            return commandCreationResult.MapToIResult();
            
        var result = await sender.Send(
            commandCreationResult.Value!, cancellationToken);

        return result.Match(
            onFailure: _ => result.MapToIResult(),
            onSuccess: () => Results.NoContent());
    }
    
    private static async Task<IResult> AddEmployeeSchedulePreference(
        [FromRoute]Guid id,
        [FromBody]AddSchedulePreferenceRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var commandCreationResult = AddSchedulePreferenceCommand
            .CreateFromRequest(id, request);
            
        if (commandCreationResult.IsFailure)
            return commandCreationResult.MapToIResult();
            
        var result = await sender.Send(
            commandCreationResult.Value!, cancellationToken);

        return result.Match(
            onFailure: _ => result.MapToIResult(),
            onSuccess: () => Results.NoContent());
    }
}