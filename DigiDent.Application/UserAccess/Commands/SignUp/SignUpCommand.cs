using DigiDent.Application.UserAccess.Commands.Shared;
using DigiDent.Domain.SharedKernel.ReturnTypes;
using MediatR;

namespace DigiDent.Application.UserAccess.Commands.SignUp;

public record SignUpCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string Role) : IRequest<Result<AuthenticationResponse>>;