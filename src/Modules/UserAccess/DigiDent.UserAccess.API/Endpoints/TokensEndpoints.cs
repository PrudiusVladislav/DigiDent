using DigiDent.Shared.Infrastructure.Api;
using DigiDent.Shared.Kernel.ReturnTypes;
using DigiDent.UserAccess.Application.Abstractions;
using DigiDent.UserAccess.Application.Commands.Refresh;
using DigiDent.UserAccess.Application.Commands.Shared;
using DigiDent.UserAccess.Application.Commands.SignIn;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace DigiDent.UserAccess.API.Endpoints;

internal static class TokensEndpoints
{
    internal static RouteGroupBuilder MapTokensEndpoints(
        this RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapPost("/sign-in", SignIn);
        groupBuilder.MapPost("/refresh", Refresh);
        
        return groupBuilder;
    }
    
    private static async Task<IResult> SignIn(
        [FromBody]SignInRequest request,
        ISender sender,
        IRoleFactory roleFactory,
        CancellationToken cancellationToken)
    {
        Result<SignInCommand> signInCommandResult = SignInCommand
            .CreateFromRequest(request, roleFactory);
        
        if (signInCommandResult.IsFailure)
            return signInCommandResult.MapToIResult();
        
        Result<AuthenticationResponse> signInResult = await sender.Send(
            signInCommandResult.Value!, cancellationToken);
        
        return signInResult.Match(
            onFailure: _ => signInResult.MapToIResult(),
            onSuccess: tokens => Results.Ok(tokens));
    }
    
    private static async Task<IResult> Refresh(
        [FromBody]RefreshCommand refreshCommand,
        ISender sender,
        CancellationToken cancellationToken)
    {
        Result<AuthenticationResponse> refreshResult = await sender.Send(
            refreshCommand, cancellationToken);
        
        return refreshResult.Match(
            onFailure: _ => refreshResult.MapToIResult(),
            onSuccess: tokens => Results.Ok(tokens));
    }
}