namespace DigiDent.Application.UserAccess.Commands.SignIn;

public sealed record SignInRequest(
    string Email,
    string Password,
    string Role);