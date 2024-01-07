using DigiDent.Domain.SharedKernel;
using Mediator;

namespace DigiDent.Application.UserAccess.Commands.SignIn;

public record SignInCommand(string Email, string Password, string Role)
    : IRequest<Result<SignInResponse>>;