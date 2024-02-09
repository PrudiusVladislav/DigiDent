using DigiDent.Shared.Kernel.ValueObjects;
using DigiDent.UserAccess.Domain.Users;
using DigiDent.UserAccess.Domain.Users.Abstractions;
using DigiDent.UserAccess.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace DigiDent.UserAccess.EFCorePersistence.Users;

public class UsersRepository: IUsersRepository
{
    private readonly UserAccessDbContext _dbContext;
    
    public UsersRepository(UserAccessDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        await _dbContext.Users.AddAsync(user, cancellationToken);
    }

    public async Task<User?> GetByIdAsync(UserId userId, CancellationToken cancellationToken)
    {
        return await _dbContext.Users
            .FindAsync(userId, cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken)
    {
        return await _dbContext.Users.SingleOrDefaultAsync(
            x => x.Email == email, cancellationToken);
    }
    
    public async Task UpdateAsync(User user, CancellationToken cancellationToken)
    {
        _dbContext.Users.Update(user);
    }
    
    public async Task DeleteAsync(UserId userId, CancellationToken cancellationToken)
    {
        var userToDelete = await _dbContext.Users.FirstOrDefaultAsync(
            x => x.Id == userId, cancellationToken);
        
        if (userToDelete == null) return;
        
        _dbContext.Users.Remove(userToDelete);
    }
}