using DigiDent.InventoryManagement.Domain.Items.ValueObjects;

namespace DigiDent.InventoryManagement.Domain.Items;

public sealed class InventoryItemsDomainService
{
    private readonly IInventoryItemsCommandsRepository _inventoryItemsCommandsRepository;

    public InventoryItemsDomainService(
        IInventoryItemsCommandsRepository inventoryItemsCommandsRepository)
    {
        _inventoryItemsCommandsRepository = inventoryItemsCommandsRepository;
    }
    
    public async Task<bool> IsItemNameUnique(
        ItemName name, CancellationToken cancellationToken)
    {
        InventoryItem? item = await _inventoryItemsCommandsRepository.GetByNameAsync(
            name, cancellationToken);

        return item is null;
    }
}