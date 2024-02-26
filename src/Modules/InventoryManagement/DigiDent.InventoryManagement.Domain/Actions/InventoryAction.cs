using DigiDent.InventoryManagement.Domain.Actions.Events;
using DigiDent.InventoryManagement.Domain.Actions.ValueObjects;
using DigiDent.InventoryManagement.Domain.Employees;
using DigiDent.InventoryManagement.Domain.Employees.ValueObjects;
using DigiDent.InventoryManagement.Domain.Items;
using DigiDent.InventoryManagement.Domain.Items.ValueObjects;
using DigiDent.Shared.Kernel.Abstractions;
using DigiDent.Shared.Kernel.Extensions;

namespace DigiDent.InventoryManagement.Domain.Actions;

public class InventoryAction :
    AggregateRoot,
    IEntity<InventoryActionId, int>
{
    public InventoryActionId Id { get; init; } = null!;
    public ActionType Type { get; init; }
    public DateOnly Date { get; init; }
    
    public EmployeeId ActionPerformerId { get; init; }
    public Employee ActionPerformer { get; init; } = null!;
    
    public InventoryItemId InventoryItemId { get; init; }
    public InventoryItem InventoryItem { get; init; } = null!;

    public Quantity Quantity { get; init; }
    
    public InventoryAction(
        ActionType type,
        EmployeeId actionPerformerId,
        InventoryItemId inventoryItemId,
        Quantity quantity)
    {
        Type = type;
        ActionPerformerId = actionPerformerId;
        Date = DateTime.Now.ToDateOnly();
        InventoryItemId = inventoryItemId;
        Quantity = quantity;
        
        Raise(new InventoryActionOccurredDomainEvent(
            TimeOfOccurrence: DateTime.Now, 
            Action: this));
    }
}