using DigiDent.Application.ClinicCore.Doctors.Queries.GetAllDoctors;
using DigiDent.Application.ClinicCore.Doctors.Queries.GetDoctorById;
using MediatR;

namespace DigiDent.API.Endpoints.ClinicCore;

public static class DoctorsEndpoints
{
    public static RouteGroupBuilder MapDoctorsEndpoints(this RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapGroup("/doctors")
            .MapGetAllDoctorsEndpoint()
            .MapGetDoctorByIdEndpoint();
        return groupBuilder;
    }
    
    private static IEndpointRouteBuilder MapGetAllDoctorsEndpoint(this IEndpointRouteBuilder app)
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
    
    private static IEndpointRouteBuilder MapGetDoctorByIdEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", async (
            Guid id,
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
}