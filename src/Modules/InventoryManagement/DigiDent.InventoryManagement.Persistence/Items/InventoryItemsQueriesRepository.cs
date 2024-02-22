using Dapper;
using DigiDent.InventoryManagement.Domain.Items;
using DigiDent.InventoryManagement.Domain.Items.ReadModels;
using DigiDent.InventoryManagement.Domain.Items.ValueObjects;
using DigiDent.InventoryManagement.Persistence.Constants;
using DigiDent.Shared.Abstractions.Factories;
using DigiDent.Shared.Kernel.ValueObjects.Pagination;
using Microsoft.Data.SqlClient;

namespace DigiDent.InventoryManagement.Persistence.Items;

public class InventoryItemsQueriesRepository: IInventoryItemsQueriesRepository
{
    private readonly IDbConnectionFactory<SqlConnection> _connectionFactory;

    public InventoryItemsQueriesRepository(
        IDbConnectionFactory<SqlConnection> connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<InventoryItemDetails?> GetByIdAsync(
        InventoryItemId id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<PaginatedResponse<InventoryItemSummary>> GetAllAsync(
        IPaginationOptions pagination, CancellationToken cancellationToken)
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
}