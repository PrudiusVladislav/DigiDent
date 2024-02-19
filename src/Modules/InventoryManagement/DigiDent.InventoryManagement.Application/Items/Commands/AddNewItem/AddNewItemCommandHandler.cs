using DigiDent.InventoryManagement.Domain.Items;
using DigiDent.InventoryManagement.Domain.Items.Errors;
using DigiDent.Shared.Abstractions.Commands;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.InventoryManagement.Application.Items.Commands.AddNewItem;

public sealed class AddNewItemCommandHandler
    : ICommandHandler<AddNewItemCommand, Result<int>>
{
    private readonly InventoryItemsDomainService _domainService;
    private readonly IInventoryItemsRepository _repository;

    public AddNewItemCommandHandler(
        InventoryItemsDomainService domainService,
        IInventoryItemsRepository repository)
    {
        _domainService = domainService;
        _repository = repository;
    }

    public async Task<Result<int>> Handle(
        AddNewItemCommand command, CancellationToken cancellationToken)
    {
        if (!await _domainService.IsItemNameUnique(command.Name, cancellationToken))
        {
            return Result.Fail<int>(InventoryItemErrors
                .ItemNameIsAlreadyTaken(command.Name.Value));
        }
        
        InventoryItem item = new(
            command.Name,
            command.Category,
            command.Quantity,
            command.Remarks);
        
        await _repository.AddAsync(item, cancellationToken);
        return Result.Ok(item.Id.Value);
    }
}