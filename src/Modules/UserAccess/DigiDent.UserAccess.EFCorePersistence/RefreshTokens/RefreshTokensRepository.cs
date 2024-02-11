using DigiDent.UserAccess.Application.Abstractions;
using DigiDent.UserAccess.Application.Tokens;
using DigiDent.UserAccess.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace DigiDent.UserAccess.EFCorePersistence.RefreshTokens;

public class RefreshTokensRepository: IRefreshTokensRepository
{
    private readonly UserAccessDbContext _dbContext;
    
    public RefreshTokensRepository(UserAccessDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddRefreshTokenAsync(
        RefreshToken refreshToken,
        CancellationToken cancellationToken)
    {
        await _dbContext.RefreshTokens.AddAsync(refreshToken, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<RefreshToken?> GetRefreshTokenAsync(
        string refreshToken,
        CancellationToken cancellationToken)
    {
        return await _dbContext.RefreshTokens
            .FindAsync(refreshToken, cancellationToken);
    }
    
    public async Task DeleteRefreshTokenAsync(
        string refreshToken,
        CancellationToken cancellationToken)
    {
        var refreshTokenToDelete = await _dbContext.RefreshTokens
            .SingleOrDefaultAsync(x => x.Token == refreshToken, cancellationToken);
        if (refreshTokenToDelete is null) return;
        
        _dbContext.RefreshTokens.Remove(refreshTokenToDelete);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task DeleteRefreshTokenByUserIdAsync(
        UserId userId,
        CancellationToken cancellationToken)
    {
        var refreshTokenToDelete = await _dbContext.RefreshTokens
            .SingleOrDefaultAsync(rt => rt.UserId == userId, cancellationToken);
        if (refreshTokenToDelete is null) return;
        
        _dbContext.RefreshTokens.Remove(refreshTokenToDelete);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}