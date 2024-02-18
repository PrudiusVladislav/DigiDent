using DigiDent.InventoryManagement.Domain.Actions.ReadModels;
using DigiDent.Shared.Kernel.ValueObjects.Pagination;

namespace DigiDent.InventoryManagement.Domain.Actions;

public interface IInventoryActionsRepository
{
    Task<PaginatedResponse<ActionSummary>> GetAllAsync(
        PaginationDTO pagination, CancellationToken cancellationToken);
    
    Task AddAsync(InventoryAction action, CancellationToken cancellationToken);
}