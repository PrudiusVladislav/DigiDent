using DigiDent.Domain.SharedKernel;
using DigiDent.Domain.UserAccessContext.Users;
using DigiDent.Domain.UserAccessContext.Users.ValueObjects;

namespace DigiDent.Application.UserAccess.Tokens;

public class RefreshToken
{
    public string Token { get; set; }
    public string JwtId { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public UserId UserId { get; set; }
    public User User { get; set; }
}