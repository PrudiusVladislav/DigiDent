using DigiDent.API.Extensions;
using DigiDent.Application.UserAccess.Abstractions;
using DigiDent.Application.UserAccess.Commands.Refresh;
using DigiDent.Application.UserAccess.Commands.Shared;
using DigiDent.Application.UserAccess.Commands.SignIn;
using DigiDent.Application.UserAccess.Commands.SignUp;
using DigiDent.Domain.SharedKernel.ReturnTypes;
using DigiDent.Domain.SharedKernel.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DigiDent.API.Endpoints.UserAccess;

public static class UserAccessEndpoints
{
    public static RouteGroupBuilder MapUserAccessEndpoints(
        this RouteGroupBuilder groupBuilder)
    {
        var userAccessGroup = groupBuilder.MapGroup("/user-access");

        userAccessGroup.MapPost("/sign-in", SignIn);
        userAccessGroup.MapPost("/employees/sign-up", EmployeesSignUp);
        userAccessGroup.MapPost("/patients/sign-up", PatientsSignUp);
        userAccessGroup.MapPost("/refresh", Refresh);

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
    
    private static async Task<IResult> EmployeesSignUp(
        [FromBody]SignUpRequest request,
        ISender sender,
        IRoleFactory roleFactory,
        CancellationToken cancellationToken)
    {
        return await SignUp(request, sender, roleFactory, cancellationToken,
            allowedRoles: roleFactory.GetEmployeeRoles());
    }
    
    private static async Task<IResult> PatientsSignUp(
        [FromBody]SignUpRequest request,
        ISender sender,
        IRoleFactory roleFactory,
        CancellationToken cancellationToken)
    {
        return await SignUp(request, sender, roleFactory, cancellationToken,
            allowedRoles: Role.Patient);
    }
    
    private static async Task<IResult> SignUp(
        SignUpRequest request,
        ISender sender,
        IRoleFactory roleFactory,
        CancellationToken cancellationToken,
        params Role[] allowedRoles)                
    {
        Result<SignUpCommand> signUpCommandResult = SignUpCommand
            .CreateFromRequest(request, roleFactory, allowedRoles);
            
        if (signUpCommandResult.IsFailure)
            return signUpCommandResult.MapToIResult();
            
        Result signUpResult = await sender.Send(
            signUpCommandResult.Value!, cancellationToken);

        return signUpResult.Match(
            onFailure: _ => signUpResult.MapToIResult(),
            onSuccess: () => Results.Ok());
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