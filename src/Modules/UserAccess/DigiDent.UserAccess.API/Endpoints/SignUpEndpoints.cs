using DigiDent.Shared.Domain.ReturnTypes;
using DigiDent.Shared.Domain.ValueObjects;
using DigiDent.Shared.Infrastructure.Api;
using DigiDent.UserAccess.Application.Abstractions;
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
        
        return groupBuilder;
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
}