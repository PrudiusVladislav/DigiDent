using DigiDent.Application.UserAccess.Abstractions;
using DigiDent.Application.UserAccess.Tokens;
using DigiDent.Domain.UserAccessContext.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace DigiDent.EFCorePersistence.UserAccess;

public class RefreshTokensRepository: IRefreshTokensRepository
{
    private readonly UserAccessDbContext _dbContext;
    
    public RefreshTokensRepository(UserAccessDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    // public async Task<RefreshToken?> GetRefreshTokenAsync(UserId userId)
    // {
    //     return await _dbContext.RefreshTokens
    //         .SingleOrDefaultAsync(x => x.UserId == userId);
    // }

    public async Task AddRefreshTokenAsync(
        RefreshToken refreshToken,
        CancellationToken cancellationToken)
    {
        await _dbContext.RefreshTokens.AddAsync(refreshToken, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteUserRefreshTokens(UserId userId)
    {
        var userRefreshTokens = _dbContext.RefreshTokens
            .Where(x => x.UserId == userId);
        _dbContext.RefreshTokens.RemoveRange(userRefreshTokens);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<RefreshToken?> GetRefreshTokenAsync(
        string refreshToken,
        CancellationToken cancellationToken)
    {
        return await _dbContext.RefreshTokens
            .SingleOrDefaultAsync(x => x.Token == refreshToken,
                cancellationToken);
    }
    
    public async Task UpdateAsync(
        RefreshToken refreshToken,
        CancellationToken cancellationToken)
    {
        _dbContext.RefreshTokens.Update(refreshToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}