using DigiDent.API.Extensions;
using DigiDent.Application.UserAccess.Commands.DeleteUser;
using DigiDent.Application.UserAccess.Commands.Refresh;
using DigiDent.Application.UserAccess.Commands.SignIn;
using DigiDent.Application.UserAccess.Commands.SignUp;
using DigiDent.Domain.SharedKernel.ValueObjects;
using MediatR;

namespace DigiDent.API.Endpoints.UserAccess;

public static class UserAccessEndpoints
{
    public static RouteGroupBuilder MapUserAccessEndpoints(this RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapGroup("/users")
            .MapSignInEndpoint()
            .MapSignUpEndpoint()
            .MapRefreshEndpoint()
            .MapDeleteUserEndpoint();
        
        return groupBuilder;
    }
    
    private static IEndpointRouteBuilder MapSignInEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/sign-in", async (
            SignInCommand signInCommand,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var signInResult = await mediator.Send(signInCommand, cancellationToken);
            return signInResult.Match(
                onFailure: _ => signInResult.MapToIResult(),
                onSuccess: response => Results.Ok(response));
        });
        
        return app;
    }

    private static IEndpointRouteBuilder MapSignUpEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/sign-up", async (
            SignUpCommand signUpCommand,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var signUpResult = await mediator.Send(signUpCommand, cancellationToken);
            return signUpResult.Match(
                onFailure: _ => signUpResult.MapToIResult(),
                onSuccess: token => Results.Ok(token));
        }).RequireRoles(Role.Administrator);
        
        return app;
    }
    
    private static IEndpointRouteBuilder MapRefreshEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/refresh", async (
            RefreshCommand refreshCommand,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var refreshResult = await mediator.Send(refreshCommand, cancellationToken);
            return refreshResult.Match(
                onFailure: _ => refreshResult.MapToIResult(),
                onSuccess: response => Results.Ok(response));
        });
        
        return app;
    }
    
    private static IEndpointRouteBuilder MapDeleteUserEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/{userId}", async (
            Guid userId,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var deleteResult = await mediator.Send(
                new DeleteUserCommand(userId), cancellationToken);
            return deleteResult.Match(
                onFailure: _ => deleteResult.MapToIResult(),
                onSuccess: () => Results.Ok());
        }).RequireRoles(Role.Administrator);
        
        return app;
    }
}