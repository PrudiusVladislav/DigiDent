namespace DigiDent.Application.UserAccess.Commands.SignIn;

public class SignInResponse
{
    public string Token { get; }
    
    public SignInResponse(string token) => Token = token;
}