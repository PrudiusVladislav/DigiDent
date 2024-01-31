using DigiDent.UserAccess.Domain.Users;
using DigiDent.UserAccess.Domain.Users.ValueObjects;

namespace DigiDent.UserAccess.Application.Tokens;

public record RefreshToken(
    string Token,
    string JwtId,
    DateTime CreationDate,
    DateTime ExpiryDate,
    UserId UserId,
    User User);