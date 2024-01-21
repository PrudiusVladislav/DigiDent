using DigiDent.API.Extensions;
using DigiDent.Application.ClinicCore.Doctors.Queries.GetAllDoctors;
using DigiDent.Application.ClinicCore.Doctors.Queries.GetAvailableTimeSlots;
using DigiDent.Application.ClinicCore.Doctors.Queries.GetDoctorById;
using DigiDent.Application.ClinicCore.Doctors.Queries.IsDoctorAvailable;
using DigiDent.Domain.SharedKernel.ReturnTypes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DigiDent.API.Endpoints.ClinicCore;

public static class DoctorsEndpoints
{
    public static RouteGroupBuilder MapDoctorsEndpoints(
        this RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapGroup("/doctors")
            .MapGetAllDoctorsEndpoint()
            .MapGetDoctorByIdEndpoint()
            .MapDoctorAvailabilityEndpoints();
        
        return groupBuilder;
    }
    
    private static IEndpointRouteBuilder MapGetAllDoctorsEndpoint(
        this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var doctors = await mediator.Send(
                new GetAllDoctorsQuery(), cancellationToken);
            return Results.Ok(doctors);
        });
        
        return app;
    }
    
    private static IEndpointRouteBuilder MapGetDoctorByIdEndpoint(
        this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", async (
            [FromRoute]Guid id,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            DoctorProfileDTO? doctor = await mediator.Send(
                new GetDoctorByIdQuery(id), cancellationToken);
            
            return doctor is null
                ? Results.NotFound()
                : Results.Ok(doctor);
        });
        
        return app;
    }
    
    private static IEndpointRouteBuilder MapDoctorAvailabilityEndpoints(
        this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}/availability/check", async (
            [FromRoute]Guid id,
            [FromQuery]DateTime dateTime,
            [FromQuery]int duration,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var query = new IsDoctorAvailableQuery(
                id, dateTime, duration);
            
            IsDoctorAvailableResponse response  = await mediator.Send(
                query, cancellationToken);
            
            return Results.Ok(response);
        });
        
        app.MapGet("/{id}/availability/slots", async (
            [FromRoute]Guid id,
            [FromQuery]DateTime from,
            [FromQuery]DateOnly until,
            [FromQuery]int duration,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var query = new GetAvailableTimeSlotsQuery(
                id, from, until, duration);
            
            IReadOnlyCollection<DateTime> response = await mediator.Send(
                query, cancellationToken);
            
            return Results.Ok(response);
        });
        
        return app;
    }
}