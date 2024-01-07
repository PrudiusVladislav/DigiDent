namespace DigiDent.Application.UserAccess.Commands.SignUp;

public class SignUpResponse
{
    public string Token { get; }
    
    public SignUpResponse(string token) => Token = token;
}