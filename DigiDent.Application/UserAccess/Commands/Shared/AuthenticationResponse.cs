namespace DigiDent.Application.UserAccess.Commands.Shared;

public class AuthenticationResponse
{
    public string AccessToken { get; }
    public string RefreshToken { get; }
    
    public AuthenticationResponse(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
}