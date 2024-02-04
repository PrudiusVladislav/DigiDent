using DigiDent.Shared.Infrastructure.Api;
using DigiDent.Shared.Kernel.ReturnTypes;
using DigiDent.Shared.Kernel.ValueObjects;
using DigiDent.UserAccess.Application.Abstractions;
using DigiDent.UserAccess.Application.Commands.Activate;
using DigiDent.UserAccess.Application.Commands.SignUp;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace DigiDent.UserAccess.API.Endpoints;

internal static class SignUpEndpoints
{ 
    internal static RouteGroupBuilder MapSignUpEndpoints(
        this RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapPost("/employees/sign-up", EmployeesSignUp);
        groupBuilder.MapPost("/patients/sign-up", PatientsSignUp);
        groupBuilder.MapPost("/activate/{id:guid}", ActivateUser);
        
        return groupBuilder;
    }
    
    private static async Task<IResult> EmployeesSignUp(
        [FromBody]SignUpRequest request,
        [FromServices]ISender sender,
        [FromServices]IRoleFactory roleFactory,
        CancellationToken cancellationToken)
    {
        return await SignUp(request, sender, roleFactory, cancellationToken,
            allowedRoles: roleFactory.GetEmployeeRoles());
    }
    
    private static async Task<IResult> PatientsSignUp(
        [FromBody]SignUpRequest request,
        [FromServices]ISender sender,
        [FromServices]IRoleFactory roleFactory,
        CancellationToken cancellationToken)
    {
        return await SignUp(request, sender, roleFactory, cancellationToken,
            allowedRoles: Role.Patient);
    }
    
    private static async Task<IResult> SignUp(
        SignUpRequest request,
        [FromServices]ISender sender,
        [FromServices]IRoleFactory roleFactory,
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
    
    private static async Task<IResult> ActivateUser(
        [FromRoute]Guid id,
        [FromServices]ISender sender,
        CancellationToken cancellationToken)
    {
        ActivateUserCommand command = new(id);
        
        Result activationResult = await sender.Send(
            command, cancellationToken);
        
        return activationResult.Match(
            onFailure: _ => activationResult.MapToIResult(),
            onSuccess: () => Results.Ok());
    }
}