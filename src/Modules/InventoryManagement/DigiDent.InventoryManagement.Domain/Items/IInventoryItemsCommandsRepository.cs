using DigiDent.InventoryManagement.Domain.Items.ReadModels;
using DigiDent.InventoryManagement.Domain.Items.ValueObjects;

namespace DigiDent.InventoryManagement.Domain.Items;

public interface IInventoryItemsCommandsRepository
{
    Task<InventoryItem?> GetByIdAsync(
        InventoryItemId id, CancellationToken cancellationToken);
    
    Task<InventoryItem?> GetByNameAsync(
        ItemName name, CancellationToken cancellationToken);
    
    Task AddAsync(InventoryItem item, CancellationToken cancellationToken);
    Task UpdateAsync(InventoryItem item, CancellationToken cancellationToken);
}