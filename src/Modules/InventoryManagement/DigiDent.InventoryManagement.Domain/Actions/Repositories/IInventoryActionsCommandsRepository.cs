
namespace DigiDent.InventoryManagement.Domain.Actions.Repositories;

public interface IInventoryActionsCommandsRepository
{
    Task AddAsync(InventoryAction action, CancellationToken cancellationToken);
}