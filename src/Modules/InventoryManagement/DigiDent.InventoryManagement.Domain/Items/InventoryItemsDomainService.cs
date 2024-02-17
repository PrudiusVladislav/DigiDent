using DigiDent.InventoryManagement.Domain.Items.Errors;
using DigiDent.InventoryManagement.Domain.Items.ValueObjects;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.InventoryManagement.Domain.Items;

public sealed class InventoryItemsDomainService
{
    private readonly IInventoryItemsRepository _inventoryItemsRepository;

    public InventoryItemsDomainService(
        IInventoryItemsRepository inventoryItemsRepository)
    {
        _inventoryItemsRepository = inventoryItemsRepository;
    }
    
    public async Task<Result<ItemName>> CreateItemName(string value)
    {
        Result<ItemName> nameValidationResult = ItemName.Create(value);
        
        if (nameValidationResult.IsFailure) 
            return nameValidationResult;
        
        InventoryItem? item = await _inventoryItemsRepository.GetByNameAsync(
            nameValidationResult.Value!);
        
        if (item is not null)
            return Result.Fail<ItemName>(InventoryItemErrors
                .ItemNameIsAlreadyTaken(value));
        
        return nameValidationResult;
    }
}