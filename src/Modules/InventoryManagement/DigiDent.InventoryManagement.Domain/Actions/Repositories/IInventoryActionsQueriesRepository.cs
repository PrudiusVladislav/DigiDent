using DigiDent.InventoryManagement.Domain.Actions.ReadModels;
using DigiDent.Shared.Kernel.Pagination;

namespace DigiDent.InventoryManagement.Domain.Actions.Repositories;

public interface IInventoryActionsQueriesRepository
{
    Task<PaginatedResponse<ActionSummary>> GetAllAsync(
        IPaginationData pagination, CancellationToken cancellationToken);
}