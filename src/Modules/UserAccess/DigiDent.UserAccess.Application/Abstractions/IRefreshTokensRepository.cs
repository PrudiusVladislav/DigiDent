using DigiDent.UserAccess.Application.Tokens;
using DigiDent.UserAccess.Domain.Users.ValueObjects;

namespace DigiDent.UserAccess.Application.Abstractions;

public interface IRefreshTokensRepository
{
    Task AddRefreshTokenAsync(RefreshToken refreshToken, CancellationToken cancellationToken);
    Task<RefreshToken?> GetRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
    Task DeleteRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
    Task DeleteRefreshTokenByUserIdAsync(UserId userId, CancellationToken cancellationToken);
}