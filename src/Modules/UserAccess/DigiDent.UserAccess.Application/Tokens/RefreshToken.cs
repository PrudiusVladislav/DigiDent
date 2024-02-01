using DigiDent.UserAccess.Domain.Users;
using DigiDent.UserAccess.Domain.Users.ValueObjects;

namespace DigiDent.UserAccess.Application.Tokens;

public record RefreshToken
{
    public string Token { get; init; } = string.Empty;
    public string JwtId { get; init; } = string.Empty;
    public DateTime CreationDate { get; init; }
    public DateTime ExpiryDate { get; init; }
    public UserId UserId { get; init; } = null!;
    public User User { get; init; } = null!;
}