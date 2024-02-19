using DigiDent.InventoryManagement.Domain.Items.ValueObjects;

namespace DigiDent.InventoryManagement.Domain.Items;

public sealed class InventoryItemsDomainService
{
    private readonly IInventoryItemsRepository _inventoryItemsRepository;

    public InventoryItemsDomainService(
        IInventoryItemsRepository inventoryItemsRepository)
    {
        _inventoryItemsRepository = inventoryItemsRepository;
    }
    
    public async Task<bool> IsItemNameUnique(
        ItemName name, CancellationToken cancellationToken)
    {
        InventoryItem? item = await _inventoryItemsRepository.GetByNameAsync(
            name, cancellationToken);

        return item is null;
    }
}