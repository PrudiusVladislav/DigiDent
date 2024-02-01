using DigiDent.UserAccess.Application.Abstractions;
using DigiDent.Shared.Abstractions.Commands;
using DigiDent.Shared.Kernel.Errors;
using DigiDent.Shared.Kernel.ReturnTypes;
using DigiDent.Shared.Kernel.ValueObjects;
using DigiDent.UserAccess.Application.Commands.Shared;

namespace DigiDent.UserAccess.Application.Commands.SignIn;

public sealed record SignInCommand
    : ICommand<Result<AuthenticationResponse>>
{
    public Role Role { get; init; }
    public Email Email { get; init; } = null!;
    public string Password { get; init; } = string.Empty;

    public static Result<SignInCommand> CreateFromRequest(
        SignInRequest request, IRoleFactory roleFactory)
    {
        Result<Email> emailResult = Email.Create(request.Email);
        if (emailResult.IsFailure)
        {
            return Result.Fail<SignInCommand>(EmailErrors
                .EmailIsNotRegistered(request.Email));
        }
        
        Result<Role> roleResult = roleFactory.CreateRole(request.Role);
        if (roleResult.IsFailure)
            return roleResult.MapToType<SignInCommand>();
        
        return Result.Ok(new SignInCommand
        {
            Role = roleResult.Value,
            Email = emailResult.Value!,
            Password = request.Password
        });
    }
}