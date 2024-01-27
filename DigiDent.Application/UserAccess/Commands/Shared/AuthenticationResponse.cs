namespace DigiDent.Application.UserAccess.Commands.Shared;

public record AuthenticationResponse(
    string AccessToken,
    string RefreshToken);