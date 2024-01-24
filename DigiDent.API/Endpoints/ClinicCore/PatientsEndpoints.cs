using DigiDent.Application.ClinicCore.Patients.Queries.GetAllPatients;
using DigiDent.Application.ClinicCore.Patients.Queries.GetPatientProfile;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DigiDent.API.Endpoints.ClinicCore;

public static class PatientsEndpoints
{
    public static RouteGroupBuilder MapPatientsEndpoints(
        this RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapGroup("/patients")
            .MapGetPatientsInfoEndpoints();

        return groupBuilder;
    }
    
    private static IEndpointRouteBuilder MapGetPatientsInfoEndpoints(
        this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var patients = await mediator.Send(
                new GetAllPatientsQuery(), cancellationToken);

            return Results.Ok(patients);
        });
        
        app.MapGet("/{id:guid}", async (
            [FromRoute]Guid id,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            PatientProfileDTO? patient = await mediator.Send(
                new GetPatientProfileQuery(id), cancellationToken);

            return patient is null
                ? Results.NotFound()
                : Results.Ok(patient);
        });
        
        return app;
    }
}