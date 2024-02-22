using DigiDent.InventoryManagement.Domain.Actions.ReadModels;
using DigiDent.Shared.Kernel.ValueObjects.Pagination;

namespace DigiDent.InventoryManagement.Domain.Actions;

public interface IInventoryActionsQueriesRepository
{
    Task<PaginatedResponse<ActionSummary>> GetAllAsync(
        IPaginationOptions pagination, CancellationToken cancellationToken);
}