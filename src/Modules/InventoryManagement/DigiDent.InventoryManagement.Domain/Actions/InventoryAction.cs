using DigiDent.InventoryManagement.Domain.Actions.Events;
using DigiDent.InventoryManagement.Domain.Actions.ValueObjects;
using DigiDent.InventoryManagement.Domain.Employees;
using DigiDent.InventoryManagement.Domain.Items;
using DigiDent.InventoryManagement.Domain.Items.ValueObjects;
using DigiDent.Shared.Kernel.Abstractions;
using DigiDent.Shared.Kernel.Extensions;

namespace DigiDent.InventoryManagement.Domain.Actions;

public class InventoryAction :
    AggregateRoot,
    IEntity<InventoryActionId, Guid>
{
    public InventoryActionId Id { get; init; }
    public ActionType Type { get; init; }
    public DateOnly Date { get; init; }
    
    public EmployeeId ActionPerformerId { get; init; } = null!;
    public Employee ActionPerformer { get; init; }
    
    public InventoryItemId? InventoryItemId { get; init; }
    public InventoryItem? InventoryItem { get; init; }
    
    public Quantity Quantity { get; init; }
    
    public InventoryAction(
        ActionType type,
        Employee actionPerformer,
        InventoryItemId inventoryItemId,
        Quantity quantity)
    {
        Id = TypedId.New<InventoryActionId>();
        Type = type;
        ActionPerformer = actionPerformer;
        Date = DateTime.Now.ToDateOnly();
        InventoryItemId = inventoryItemId;
        Quantity = quantity;
        
        Raise(new InventoryActionOccurred(
            TimeOfOccurrence: DateTime.Now, 
            Action: this));
    }
}