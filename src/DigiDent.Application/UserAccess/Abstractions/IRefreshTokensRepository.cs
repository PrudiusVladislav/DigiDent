using DigiDent.Application.UserAccess.Tokens;
using DigiDent.Domain.UserAccessContext.Users.ValueObjects;

namespace DigiDent.Application.UserAccess.Abstractions;

public interface IRefreshTokensRepository
{
    Task AddRefreshTokenAsync(RefreshToken refreshToken, CancellationToken cancellationToken);
    Task<RefreshToken?> GetRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
    Task DeleteRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
    Task DeleteRefreshTokenByUserIdAsync(UserId userId, CancellationToken cancellationToken);
}