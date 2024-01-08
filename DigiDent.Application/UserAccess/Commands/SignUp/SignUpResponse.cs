
namespace DigiDent.Application.UserAccess.Commands.SignUp;

public class SignUpResponse
{
    public Guid UserId { get; }
    
    public SignUpResponse(Guid userId) => UserId = userId;
}