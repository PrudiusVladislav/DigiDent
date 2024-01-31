namespace DigiDent.UserAccess.Application.Commands.SignIn;

public sealed record SignInRequest(
    string Email,
    string Password,
    string Role);