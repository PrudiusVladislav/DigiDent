using DigiDent.API.Extensions;
using DigiDent.Application.UserAccess.Commands.SignIn;
using DigiDent.Application.UserAccess.Commands.SignUp;
using DigiDent.Domain.UserAccessContext.Users.ValueObjects;
using DigiDent.Infrastructure.UserAccess;
using Mediator;
using Microsoft.AspNetCore.Identity.Data;

namespace DigiDent.API.Endpoints.UserAccess;

public static class UserAccessEndpoints
{
    public static void MapUserAccessEndpoints(this WebApplication app)
    {
        app.MapGroup("/users")
            .MapSignInEndpoint()
            .MapSignUpEndpoint();
    }
    
    private static IEndpointRouteBuilder MapSignInEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/sign-in", async (
            SignInCommand loginRequest,
            ISender mediator,
            CancellationToken cancellationToken) =>
        {
            var signInResult = await mediator.Send(loginRequest, cancellationToken);
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
            ISender mediator,
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