using DigiDent.InventoryManagement.Domain.Requests.ValueObjects;

namespace DigiDent.InventoryManagement.Domain.Requests;

public interface IRequestsCommandsRepository
{
    Task<Request?> GetByIdAsync(RequestId id, CancellationToken cancellationToken);
    
    Task AddAsync(Request request, CancellationToken cancellationToken);
    Task UpdateAsync(Request request, CancellationToken cancellationToken);
    Task DeleteAsync(RequestId id, CancellationToken cancellationToken);
}