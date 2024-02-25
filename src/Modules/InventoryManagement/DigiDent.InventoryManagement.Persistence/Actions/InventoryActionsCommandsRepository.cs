using DigiDent.InventoryManagement.Domain.Actions;
using DigiDent.InventoryManagement.Domain.Actions.Repositories;

namespace DigiDent.InventoryManagement.Persistence.Actions;

public class InventoryActionsCommandsRepository: IInventoryActionsCommandsRepository
{
    private readonly InventoryManagementDbContext _dbContext;

    public InventoryActionsCommandsRepository(
        InventoryManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(
        InventoryAction action, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(action, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}