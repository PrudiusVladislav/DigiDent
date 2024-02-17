using DigiDent.InventoryManagement.Domain.Items.ValueObjects;

namespace DigiDent.InventoryManagement.Domain.Items;

public interface IInventoryItemsRepository
{
    Task<InventoryItem?> GetByIdAsync(InventoryItemId id);
    Task<InventoryItem?> GetByNameAsync(ItemName name);
}