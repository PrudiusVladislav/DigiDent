﻿using DigiDent.UserAccess.Application.Abstractions;
using DigiDent.Shared.Abstractions.Commands;
using DigiDent.Shared.Kernel.ReturnTypes;
using DigiDent.Shared.Kernel.ValueObjects;
using DigiDent.UserAccess.Domain.Users.ValueObjects;

namespace DigiDent.UserAccess.Application.Commands.SignUp;

public sealed record SignUpCommand: ICommand<Result>
{
    public FullName FullName { get; init; } = null!;
    public Email Email { get; init; } = null!;
    public PhoneNumber PhoneNumber { get; init; } = null!;
    public Password Password { get; init; } = null!;
    public Role Role { get; init; }
    
    public static Result<SignUpCommand> CreateFromRequest(
        SignUpRequest request,
        IRoleFactory roleFactory,
        params Role[] allowedRoles)
    {
        var fullNameResult = FullName.Create(request.FirstName, request.LastName);
        var emailResult = Email.Create(request.Email);
        var phoneNumberResult = PhoneNumber.Create(request.PhoneNumber);
        var passwordResult = Password.Create(request.Password);
        var roleResult = roleFactory.CreateRole(request.Role, allowedRoles);
        
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