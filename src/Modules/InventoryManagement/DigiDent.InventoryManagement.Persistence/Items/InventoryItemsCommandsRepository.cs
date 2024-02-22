using DigiDent.InventoryManagement.Domain.Items;
using DigiDent.InventoryManagement.Domain.Items.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace DigiDent.InventoryManagement.Persistence.Items;

public class InventoryItemsCommandsRepository: IInventoryItemsCommandsRepository
{
    private readonly InventoryManagementDbContext _dbContext;

    public InventoryItemsCommandsRepository(
        InventoryManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<InventoryItem?> GetByIdAsync(
        InventoryItemId id, CancellationToken cancellationToken)
    {
        return await _dbContext.Items
            .FindAsync([id], cancellationToken: cancellationToken);
    }

    public async Task<InventoryItem?> GetByNameAsync(
        ItemName name, CancellationToken cancellationToken)
    {
        return await _dbContext.Items
            .FirstOrDefaultAsync(i => i.Name == name, cancellationToken);
    }

    public async Task AddAsync(
        InventoryItem item, CancellationToken cancellationToken)
    {
        await _dbContext.Items.AddAsync(item, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(
        InventoryItem item, CancellationToken cancellationToken)
    {
        _dbContext.Items.Update(item);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}