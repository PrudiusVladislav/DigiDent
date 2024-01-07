using DigiDent.Domain.SharedKernel;
using Mediator;

namespace DigiDent.Application.UserAccess.Commands.SignUp;

public record SignUpCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string Role) : IRequest<Result<SignUpResponse>>;