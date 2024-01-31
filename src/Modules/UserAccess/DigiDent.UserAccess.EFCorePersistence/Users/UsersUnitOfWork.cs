using DigiDent.Domain.UserAccessContext.Users;

namespace DigiDent.EFCorePersistence.UserAccess.Users;

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

    public void Dispose()
    {
        _dbContext.Dispose();
        GC.SuppressFinalize(this);
    }
}