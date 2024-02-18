using DigiDent.InventoryManagement.Domain.Requests.ReadModels;
using DigiDent.InventoryManagement.Domain.Requests.ValueObjects;
using DigiDent.Shared.Kernel.ValueObjects.Pagination;

namespace DigiDent.InventoryManagement.Domain.Requests;

public interface IRequestsRepository
{
    Task<Request?> GetByIdAsync(RequestId id, CancellationToken cancellationToken);
    
    Task<PaginatedResponse<RequestSummary>> GetAllAsync(
        PaginationDTO paginationOptions, CancellationToken cancellationToken);
    
    Task AddAsync(Request request, CancellationToken cancellationToken);
    Task UpdateAsync(Request request, CancellationToken cancellationToken);
    Task DeleteAsync(RequestId id, CancellationToken cancellationToken);
}