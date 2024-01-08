using DigiDent.Domain.SharedKernel;
using MediatR;

namespace DigiDent.Application.UserAccess.Commands.SignIn;

public record SignInCommand(string Email, string Password, string Role)
    : IRequest<Result<SignInResponse>>;