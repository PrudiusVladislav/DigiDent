﻿using DigiDent.InventoryManagement.Domain.Actions;
using DigiDent.InventoryManagement.Domain.Actions.Repositories;
using DigiDent.InventoryManagement.Domain.Actions.ValueObjects;
using DigiDent.InventoryManagement.Domain.Items;
using DigiDent.InventoryManagement.Domain.Items.Repositories;
using DigiDent.InventoryManagement.Domain.Items.ValueObjects;
using DigiDent.Shared.Abstractions.Commands;
using DigiDent.Shared.Abstractions.Errors;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.InventoryManagement.Application.Actions.Commands.MakeInventoryAction;

public class MakeInventoryActionCommandHandler
    : ICommandHandler<MakeInventoryActionCommand, Result<int>>
{
    private readonly IInventoryActionsCommandsRepository _inventoryActionsCommandsRepository;
    private readonly IInventoryItemsCommandsRepository _inventoryItemsCommandsRepository;

    public MakeInventoryActionCommandHandler(
        IInventoryActionsCommandsRepository inventoryActionsCommandsRepository,
        IInventoryItemsCommandsRepository inventoryItemsCommandsRepository)
    {
        _inventoryActionsCommandsRepository = inventoryActionsCommandsRepository;
        _inventoryItemsCommandsRepository = inventoryItemsCommandsRepository;
    }

    public async Task<Result<int>> Handle(
        MakeInventoryActionCommand command, CancellationToken cancellationToken)
    {
        InventoryItem? item = await _inventoryItemsCommandsRepository
            .GetByIdAsync(command.ItemId, cancellationToken);

        if (item is null)
        {
            return Result.Fail<int>(RepositoryErrors
                .EntityNotFound<InventoryItem, int>(command.ItemId));
        }

        Result quantityChangeResult = HandleQuantityChange(
            item, command.Type, command.Quantity);

        if (quantityChangeResult.IsFailure)
            return quantityChangeResult.MapToType<int>();
        
        InventoryAction action = new(
            command.Type,
            command.PerformerId,
            command.ItemId,
            command.Quantity);
        
        await _inventoryActionsCommandsRepository.AddAsync(action, cancellationToken);
        return Result.Ok(action.Id.Value);
    }
    
    private static Result HandleQuantityChange(
        InventoryItem item, ActionType type, Quantity quantity)
    {
        Result result = type switch
        {
            ActionType.Addition => item.Quantity.Add(quantity),
            ActionType.Subtraction => item.Quantity.Subtract(quantity),
            _ => throw new NotSupportedException("Given action type is not supported.")
        };

        return result;
    }
}