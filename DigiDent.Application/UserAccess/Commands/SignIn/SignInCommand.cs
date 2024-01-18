using DigiDent.Application.Shared.Abstractions;
using DigiDent.Application.UserAccess.Commands.Shared;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Application.UserAccess.Commands.SignIn;

public record SignInCommand(string Email, string Password, string Role)
    : ICommand<Result<AuthenticationResponse>>;