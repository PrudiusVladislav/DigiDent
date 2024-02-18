using Dapper;
using DigiDent.InventoryManagement.Domain.Items;
using DigiDent.InventoryManagement.Domain.Items.ReadModels;
using DigiDent.InventoryManagement.Domain.Items.ValueObjects;
using DigiDent.InventoryManagement.Persistence.Constants;
using DigiDent.Shared.Abstractions.Factories;
using DigiDent.Shared.Kernel.ValueObjects.Pagination;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DigiDent.InventoryManagement.Persistence.Items;

public class InventoryItemsRepository: IInventoryItemsRepository
{
    private readonly InventoryManagementDbContext _dbContext;
    private readonly IDbConnectionFactory<SqlConnection> _connectionFactory;

    public InventoryItemsRepository(
        InventoryManagementDbContext dbContext,
        IDbConnectionFactory<SqlConnection> connectionFactory)
    {
        _dbContext = dbContext;
        _connectionFactory = connectionFactory;
    }

    public async Task<InventoryItem?> GetByIdAsync(
        InventoryItemId id, CancellationToken cancellationToken)
    {
        return await _dbContext.Items
            .FindAsync([id], cancellationToken: cancellationToken);
    }

    public async Task<InventoryItem?> GetByNameAsync(
        ItemName name, CancellationToken cancellationToken)
    {
        return await _dbContext.Items
            .FirstOrDefaultAsync(i => i.Name == name, cancellationToken);
    }

    public async Task<PaginatedResponse<InventoryItemSummary>> GetAllAsync(
        PaginationDTO pagination, CancellationToken cancellationToken)
    {
        const string schema = ConfigurationConstants.InventoryManagementSchema;
        const string sql = $@"
            SELECT [Id], [Name], [Quantity], [Category]
            FROM {schema}.[Items]
            ORDER BY @OrderByColumn @SortDirection
            WHERE [Id] > @Cursor
            LIMIT @PageSize";

        await using var connection = _connectionFactory.CreateOpenConnection();
        
        IReadOnlyCollection<InventoryItemSummary> items = (await connection
                .QueryAsync<InventoryItemSummary>(
                    sql, new 
                    {
                        OrderByColumn = pagination.SortByColumn,
                        SortDirection = pagination.SortOrder,
                        Cursor = pagination.PageSize * (pagination.PageNumber - 1),
                        PageSize = pagination.PageSize 
                    }))
            .Select(item => item with
            {
                Category = Enum
                    .Parse<ItemCategory>(item.Category)
                    .ToString()
            })
            .Where(item => item.Contains(pagination.SearchTerm))
            .ToList()
            .AsReadOnly();
        
        return new PaginatedResponse<InventoryItemSummary>(
            DataCollection: items,
            TotalCount: items.Count);
    }

    public async Task AddAsync(
        InventoryItem item, CancellationToken cancellationToken)
    {
        await _dbContext.Items.AddAsync(item, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(
        InventoryItem item, CancellationToken cancellationToken)
    {
        _dbContext.Items.Update(item);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}