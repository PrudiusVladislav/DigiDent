using DigiDent.API.Extensions;
using DigiDent.Application.UserAccess.Commands.SignIn;
using DigiDent.Application.UserAccess.Commands.SignUp;
using DigiDent.Domain.UserAccessContext.Users.ValueObjects;
using DigiDent.Infrastructure.UserAccess;
using MediatR;

namespace DigiDent.API.Endpoints.UserAccess;

public static class UserAccessEndpoints
{
    public static void MapUserAccessEndpoints(this WebApplication app)
    {
        app.MapGroup("/users")
            .MapSignInEndpoint()
            .MapSignUpEndpoint()
            .TestEndpoint();
    }
    
    private static IEndpointRouteBuilder MapSignInEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/sign-in", async (
            SignInCommand signInRequest,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var signInResult = await mediator.Send(signInRequest, cancellationToken);
            return signInResult.Match(
                onFailure: _ => signInResult.MapFailureToIResult(),
                onSuccess: token => Results.Ok(token));
        });
        
        return app;
    }

    private static IEndpointRouteBuilder MapSignUpEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/sign-up", async (
            SignUpCommand signUpRequest,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var signUpResult = await mediator.Send(signUpRequest, cancellationToken);
            return signUpResult.Match(
                onFailure: _ => signUpResult.MapFailureToIResult(),
                onSuccess: token => Results.Ok(token));
        });
        
        return app;
    }
    
    private static void TestEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/test", () => Results.Ok("Success!"))
            .RequireRoles(Role.Administrator, Role.Doctor);
    }
}