using DigiDent.Shared.Kernel.ReturnTypes;
using DigiDent.UserAccess.Domain.Users.ValueObjects;

namespace DigiDent.UserAccess.Application.Commands.SignIn;

public static class SignInErrors
{
    public static Error UserAccountIsNotActivated(Guid userId)
        => new (
            ErrorType.Validation,
            nameof(SignInCommandHandler),
            $"User account with id {userId} is not activated. To sign in" + 
            $", the account must be activated.");
    
}