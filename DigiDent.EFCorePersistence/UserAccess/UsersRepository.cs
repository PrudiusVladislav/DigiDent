using DigiDent.Domain.UserAccessContext.Users;
using DigiDent.Domain.UserAccessContext.Users.DTO;
using DigiDent.Domain.UserAccessContext.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace DigiDent.EFCorePersistence.UserAccess;

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
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<User?> GetByIdAsync(UserId userId, CancellationToken cancellationToken)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(
            x => x.Id == userId, cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(Email userName, CancellationToken cancellationToken)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(
            x => x.Email == userName, cancellationToken);
    }

    public async Task UpdateAsync(UpdateUserDto newUserDto, CancellationToken cancellationToken)
    {
        var userToUpdate = await _dbContext.Users.FirstOrDefaultAsync(
            x => x.Id == newUserDto.Id, cancellationToken);
        
        if (userToUpdate == null) return;
        
        userToUpdate.Update(newUserDto);
        
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}