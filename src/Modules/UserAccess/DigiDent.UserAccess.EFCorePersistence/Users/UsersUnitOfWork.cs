using DigiDent.UserAccess.Domain.Users;
using DigiDent.UserAccess.Domain.Users.Abstractions;

namespace DigiDent.UserAccess.EFCorePersistence.Users;

public class UsersUnitOfWork: IUsersUnitOfWork
{
    private readonly UserAccessDbContext _dbContext;

    public UsersUnitOfWork(
        IUsersRepository usersRepository,
        UserAccessDbContext dbContext)
    {
        UsersRepository = usersRepository;
        _dbContext = dbContext;
    }

    public IUsersRepository UsersRepository { get; }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
        await _dbContext.Database.CommitTransactionAsync(cancellationToken);
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.Database.RollbackTransactionAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    public void Dispose()
    {
        _dbContext.Dispose();
        GC.SuppressFinalize(this);
    }
}