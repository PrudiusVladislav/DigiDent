using DigiDent.Application.UserAccess.Tokens;
using DigiDent.Domain.UserAccessContext.Users.ValueObjects;

namespace DigiDent.Application.UserAccess.Abstractions;

public interface IRefreshTokensRepository
{
    //Task<RefreshToken?> GetRefreshTokenAsync(UserId userId, CancellationToken cancellationToken);
    Task AddRefreshTokenAsync(RefreshToken refreshToken, CancellationToken cancellationToken);
    // Task DeleteUserRefreshTokenAsync(UserId userId,);
    Task<RefreshToken?> GetRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
    Task UpdateAsync(RefreshToken refreshToken, CancellationToken cancellationToken);
}