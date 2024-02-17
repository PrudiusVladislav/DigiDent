using DigiDent.InventoryManagement.Domain.Actions.Events;
using DigiDent.InventoryManagement.Domain.Actions.ValueObjects;
using DigiDent.InventoryManagement.Domain.Shared;
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
    public Employee ActionPerformer { get; init; }
    
    public InventoryAction(
        ActionType type,
        Employee actionPerformer)
    {
        Id = TypedId.New<InventoryActionId>();
        Type = type;
        ActionPerformer = actionPerformer;
        Date = DateTime.Now.ToDateOnly();
        
        Raise(new InventoryActionOccurred(
            TimeOfOccurrence: DateTime.Now, 
            Action: this));
    }
}