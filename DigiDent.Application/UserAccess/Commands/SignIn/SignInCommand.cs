using DigiDent.Application.Shared.Abstractions;
using DigiDent.Application.UserAccess.Commands.Shared;
using DigiDent.Domain.SharedKernel.Errors;
using DigiDent.Domain.SharedKernel.ReturnTypes;
using DigiDent.Domain.SharedKernel.ValueObjects;
using DigiDent.Domain.UserAccessContext.Users.ValueObjects;

namespace DigiDent.Application.UserAccess.Commands.SignIn;

public sealed record SignInCommand
    : ICommand<Result<AuthenticationResponse>>
{
    public Role Role { get; init; }
    public Email Email { get; init; } = null!;
    public string Password { get; init; } = string.Empty;

    public static Result<SignInCommand> CreateFromRequest(
        SignInRequest request)
    {
        var emailResult = Email.Create(request.Email);
        if (emailResult.IsFailure)
        {
            return Result.Fail<SignInCommand>(EmailErrors
                .EmailIsNotRegistered(request.Email));
        }
        
        var roleResult = RoleFactory.CreateRole(request.Role);
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