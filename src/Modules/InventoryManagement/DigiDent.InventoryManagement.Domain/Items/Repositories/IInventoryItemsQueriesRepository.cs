using DigiDent.InventoryManagement.Domain.Items.ReadModels;
using DigiDent.InventoryManagement.Domain.Items.ValueObjects;
using DigiDent.Shared.Kernel.Pagination;

namespace DigiDent.InventoryManagement.Domain.Items.Repositories;

public interface IInventoryItemsQueriesRepository
{
    Task<InventoryItemDetails?> GetByIdAsync(
        InventoryItemId id, CancellationToken cancellationToken);
    
    Task<PaginatedResponse<InventoryItemSummary>> GetAllAsync(
        IPaginationOptions pagination, CancellationToken cancellationToken);
}