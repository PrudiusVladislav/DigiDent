using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DigiDent.API.Extensions;
using DigiDent.Application.UserAccess.Commands.Refresh;
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
            .MapRefreshEndpoint()
            .TestEndpoint();
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
                onFailure: _ => signInResult.MapFailureToIResult(),
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
                onFailure: _ => signUpResult.MapFailureToIResult(),
                onSuccess: token => Results.Ok(token));
        });
        
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
                onFailure: _ => refreshResult.MapFailureToIResult(),
                onSuccess: response => Results.Ok(response));
        });
        
        return app;
    }
    
    private static void TestEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/test", (HttpContext context) =>
            {
                var expirationTime = context.User
                    .FindFirstValue(JwtRegisteredClaimNames.Exp);
                var isExpired = long.Parse(expirationTime!) < DateTime.UtcNow.Ticks;
                
                var expiryDateUnix =
                    long.Parse(context.User
                        .FindFirstValue(JwtRegisteredClaimNames.Exp)!);
                var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                    .AddSeconds(expiryDateUnix);
        
                if (expiryDateTimeUtc > DateTime.UtcNow)
                    return Results.Ok("Token is not expired");
                return Results.Ok($"Is expired: {isExpired}");
            })
            .RequireRoles(Role.Administrator, Role.Doctor);
    }
}