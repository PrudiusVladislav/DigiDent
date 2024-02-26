using DigiDent.InventoryManagement.Domain.Requests.ReadModels;
using DigiDent.Shared.Kernel.Pagination;

namespace DigiDent.InventoryManagement.Domain.Requests.Repositories;

public interface IRequestsQueriesRepository
{
    Task<PaginatedResponse<RequestSummary>> GetAllAsync(
        IPaginationData paginationData, CancellationToken cancellationToken);
}