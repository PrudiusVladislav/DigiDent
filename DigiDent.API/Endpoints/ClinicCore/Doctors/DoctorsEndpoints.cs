using AutoMapper;
using DigiDent.API.Extensions;
using DigiDent.Application.ClinicCore.Doctors.Queries.GetAllDoctors;
using MediatR;

namespace DigiDent.API.Endpoints.ClinicCore.Doctors;

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
            IMapper mapper,
            CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(new GetAllDoctorsQuery(), cancellationToken);
            return result.Match(
                onFailure: _ => result.MapToIResult(),
                onSuccess: response => Results.Ok(response));
        });
        
        return app;
    }
}