using Dapper;
using DigiDent.InventoryManagement.Domain.Items.ValueObjects;
using DigiDent.InventoryManagement.Domain.Requests;
using DigiDent.InventoryManagement.Domain.Requests.ReadModels;
using DigiDent.InventoryManagement.Domain.Requests.ValueObjects;
using DigiDent.InventoryManagement.Persistence.Constants;
using Microsoft.Data.SqlClient;

namespace DigiDent.InventoryManagement.Persistence.Requests;

public class RequestsCommandsRepository: IRequestsCommandsRepository
{
    private readonly InventoryManagementDbContext _dbContext;

    public RequestsCommandsRepository(
        InventoryManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Request?> GetByIdAsync(
        RequestId id, CancellationToken cancellationToken)
    {
        return await _dbContext.Requests
            .FindAsync([id], cancellationToken);
    }

    public async Task AddAsync(Request request, CancellationToken cancellationToken)
    {
        await _dbContext.Requests.AddAsync(request, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Request request, CancellationToken cancellationToken)
    {
        _dbContext.Requests.Update(request);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(RequestId id, CancellationToken cancellationToken)
    {
        var request = await GetByIdAsync(id, cancellationToken);
        if (request is not null)
        {
            _dbContext.Requests.Remove(request);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}