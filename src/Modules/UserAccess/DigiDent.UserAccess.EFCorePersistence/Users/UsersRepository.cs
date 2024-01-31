﻿using DigiDent.Shared.Domain.ValueObjects;
using DigiDent.Domain.UserAccessContext.Users;
using DigiDent.Domain.UserAccessContext.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace DigiDent.EFCorePersistence.UserAccess.Users;

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
        return await _dbContext.Users.FirstOrDefaultAsync(
            x => x.Id == userId, cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(
            x => x.Email == email, cancellationToken);
    }
    
    public async Task DeleteAsync(UserId userId, CancellationToken cancellationToken)
    {
        var userToDelete = await _dbContext.Users.FirstOrDefaultAsync(
            x => x.Id == userId, cancellationToken);
        
        if (userToDelete == null) return;
        
        _dbContext.Users.Remove(userToDelete);
    }
}