﻿using DigiDent.Application.Shared.Abstractions;
using DigiDent.Application.UserAccess.Commands.Shared;
using DigiDent.Domain.SharedKernel.ReturnTypes;
using DigiDent.Domain.SharedKernel.ValueObjects;
using DigiDent.Domain.UserAccessContext.Users.ValueObjects;
using MediatR;

namespace DigiDent.Application.UserAccess.Commands.SignUp;

public sealed record SignUpCommand: ICommand<Result>
{
    public FullName FullName { get; init; } = null!;
    public Email Email { get; init; } = null!;
    public PhoneNumber PhoneNumber { get; init; } = null!;
    public Password Password { get; init; } = null!;
    public Role Role { get; init; }
    
    public static Result<SignUpCommand> CreateFromRequest(
        SignUpRequest request, params Role[] allowedRoles)
    {
        var fullNameResult = FullName.Create(request.FirstName, request.LastName);
        var emailResult = Email.Create(request.Email);
        var phoneNumberResult = PhoneNumber.Create(request.PhoneNumber);
        var passwordResult = Password.Create(request.Password);
        var roleResult = RoleFactory.CreateRole(request.Role, allowedRoles);
        
        var validationResult = Result.Merge(
            fullNameResult, emailResult, phoneNumberResult, passwordResult, roleResult);

        if (validationResult.IsFailure)
            return validationResult.MapToType<SignUpCommand>();
        
        return Result.Ok(new SignUpCommand
        {
            FullName = fullNameResult.Value!,
            Email = emailResult.Value!,
            PhoneNumber = phoneNumberResult.Value!,
            Password = passwordResult.Value!,
            Role = roleResult.Value
        });
    }
}