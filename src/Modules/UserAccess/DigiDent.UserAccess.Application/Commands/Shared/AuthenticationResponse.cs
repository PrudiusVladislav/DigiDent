namespace DigiDent.UserAccess.Application.Commands.Shared;

public record AuthenticationResponse(
    string AccessToken,
    string RefreshToken);