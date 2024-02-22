
namespace DigiDent.InventoryManagement.Domain.Actions;

public interface IInventoryActionsCommandsRepository
{
    Task AddAsync(InventoryAction action, CancellationToken cancellationToken);
}