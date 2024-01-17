using DigiDent.Application.UserAccess.Commands.Shared;
using DigiDent.Domain.SharedKernel.ReturnTypes;
using MediatR;

namespace DigiDent.Application.UserAccess.Commands.SignUp;

public record SignUpCommand(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Password,
    string Role) : IRequest<Result<AuthenticationResponse>>;