using DigiDent.API.Extensions;
using DigiDent.Application.UserAccess.Commands.Refresh;
using DigiDent.Application.UserAccess.Commands.SignIn;
using DigiDent.Application.UserAccess.Commands.SignUp;
using DigiDent.Domain.SharedKernel.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DigiDent.API.Endpoints.UserAccess;

public static class UserAccessEndpoints
{
    public static RouteGroupBuilder MapUserAccessEndpoints(
        this RouteGroupBuilder groupBuilder)
    {
        groupBuilder
            .MapSignInEndpoint()
            .MapSignUpEndpoints()
            .MapRefreshEndpoint();
        
        return groupBuilder;
    }
    
    private static IEndpointRouteBuilder MapSignInEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/users/sign-in", async (
            SignInCommand signInCommand,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var signInResult = await mediator.Send(signInCommand, cancellationToken);
            
            return signInResult.Match(
                onFailure: _ => signInResult.MapToIResult(),
                onSuccess: tokens => Results.Ok(tokens));
        });
        
        return app;
    }

    private static IEndpointRouteBuilder MapSignUpEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/employees/sign-up", async (
                [FromBody] SignUpRequest request, 
                IMediator mediator, 
                CancellationToken cancellationToken) 
            => await SignUp(request, mediator, cancellationToken,
                allowedRoles: RoleFactory.EmployeeRoles))
            .RequireRoles(Role.Administrator);;

        app.MapPost("/patients/sign-up", async (
                [FromBody] SignUpRequest request, 
                IMediator mediator, 
                CancellationToken cancellationToken) 
            => await SignUp(request, mediator, cancellationToken,
                allowedRoles: Role.Patient));
        
        return app;
    }
    
    private static async Task<IResult> SignUp(
        SignUpRequest request,
        IMediator mediator,
        CancellationToken cancellationToken,
        params Role[] allowedRoles)                
    {
        var signUpCommandResult = SignUpCommand.CreateFromRequest(
            request, allowedRoles);
            
        if (signUpCommandResult.IsFailure)
            return signUpCommandResult.MapToIResult();
            
        var signUpResult = await mediator.Send(
            signUpCommandResult.Value!, cancellationToken);

        return signUpResult.Match(
            onFailure: _ => signUpResult.MapToIResult(),
            onSuccess: () => Results.Ok());
    }
    
    private static IEndpointRouteBuilder MapRefreshEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/refresh", async (
            [FromBody]RefreshCommand refreshCommand,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var refreshResult = await mediator.Send(refreshCommand, cancellationToken);
            
            return refreshResult.Match(
                onFailure: _ => refreshResult.MapToIResult(),
                onSuccess: tokens => Results.Ok(tokens));
        });
        
        return app;
    }
}