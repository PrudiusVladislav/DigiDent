using DigiDent.Shared.Domain.Abstractions;
using DigiDent.UserAccess.Domain.Users;
using DigiDent.UserAccess.Domain.Users.ValueObjects;

namespace DigiDent.Application.UserAccess.Tokens;

public record RefreshToken
{
    public string Token { get; set; }
    public string JwtId { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public UserId UserId { get; set; }
    public User User { get; set; }
}