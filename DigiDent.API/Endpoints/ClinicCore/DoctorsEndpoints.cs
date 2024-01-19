using DigiDent.Application.ClinicCore.Doctors.Queries.GetAllDoctors;
using MediatR;

namespace DigiDent.API.Endpoints.ClinicCore;

public static class DoctorsEndpoints
{
    public static RouteGroupBuilder MapDoctorsEndpoints(this RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapGroup("/doctors");
        return groupBuilder;
    }
    
    private static IEndpointRouteBuilder MapGetAllDoctorsEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            IReadOnlyCollection<DoctorDTO> result = await mediator.Send(
                new GetAllDoctorsQuery(), cancellationToken);
            return Results.Ok(result);
        });
        
        return app;
    }
}