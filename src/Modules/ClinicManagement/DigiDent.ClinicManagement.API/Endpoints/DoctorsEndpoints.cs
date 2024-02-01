using DigiDent.ClinicManagement.Application.Doctors.Commands.Update;
using DigiDent.ClinicManagement.Application.Doctors.Queries.GetAllDoctors;
using DigiDent.ClinicManagement.Application.Doctors.Queries.GetAvailableTimeSlots;
using DigiDent.ClinicManagement.Application.Doctors.Queries.GetDoctorById;
using DigiDent.ClinicManagement.Application.Doctors.Queries.IsDoctorAvailable;
using DigiDent.Shared.Infrastructure.Api;
using DigiDent.Shared.Kernel.ReturnTypes;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace DigiDent.ClinicManagement.API.Endpoints;

internal static class DoctorsEndpoints
{
    internal static IEndpointRouteBuilder MapDoctorsEndpoints(
        this IEndpointRouteBuilder builder)
    {
        var doctorsGroup = builder.MapGroup("/doctors");
        
        doctorsGroup.MapGet("/", GetAllDoctorsInfo);
        doctorsGroup.MapGet("/{id}", GetDoctorProfile);
        doctorsGroup.MapGet("/{id}/availability/check", CheckDoctorAvailability);
        doctorsGroup.MapGet("/{id}/availability/slots", GetAvailableTimeSlots);
        doctorsGroup.MapPut("/{id}", UpdateDoctor);
        
        return builder;
    }
    
    private static async Task<IResult> GetAllDoctorsInfo(
        ISender sender,
        CancellationToken cancellationToken)
    {
        var doctors = await sender.Send(
            new GetAllDoctorsQuery(), cancellationToken);
        return Results.Ok(doctors);
    }
    
    private static async Task<IResult> GetDoctorProfile(
        [FromRoute]Guid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        DoctorProfileDTO? doctor = await sender.Send(
            new GetDoctorByIdQuery(id), cancellationToken);
        
        return doctor is null
            ? Results.NotFound()
            : Results.Ok(doctor);
    }
    
    private static async Task<IResult> CheckDoctorAvailability(
        [FromRoute]Guid id,
        [FromQuery]DateTime dateTime,
        [FromQuery]int duration,
        ISender sender,
        CancellationToken cancellationToken)
    {
        IsDoctorAvailableQuery query = new(id, dateTime, duration);
        
        Result<IsDoctorAvailableResponse> result  = await sender.Send(
            query, cancellationToken);
        
        return result.Match(
            onFailure: _ => result.MapToIResult(),
            onSuccess: response => Results.Ok(response));
    }
    
    private static async Task<IResult> GetAvailableTimeSlots(
        [FromRoute]Guid id,
        [FromQuery]DateTime from,
        [FromQuery]DateOnly until,
        [FromQuery]int duration,
        ISender sender,
        CancellationToken cancellationToken)
    {
        GetAvailableTimeSlotsQuery query = new(id, from, until, duration);
        
        IReadOnlyCollection<DateTime> response = await sender.Send(
            query, cancellationToken);
        
        return Results.Ok(response);
    }
    
    private static async Task<IResult> UpdateDoctor(
        [FromRoute]Guid id,
        [FromBody]UpdateDoctorRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        Result<UpdateDoctorCommand> commandResult = UpdateDoctorCommand
            .CreateFromRequest(request, doctorToUpdateId: id);

        if (commandResult.IsFailure)
            return commandResult.MapToIResult();
        
        Result result = await sender.Send(
            commandResult.Value!, cancellationToken);

        return result.Match(
            onFailure: _ => result.MapToIResult(),
            onSuccess: () => Results.NoContent());
    }
}