using Dapper;
using DigiDent.InventoryManagement.Domain.Items.ValueObjects;
using DigiDent.InventoryManagement.Domain.Requests;
using DigiDent.InventoryManagement.Domain.Requests.ReadModels;
using DigiDent.InventoryManagement.Domain.Requests.ValueObjects;
using DigiDent.InventoryManagement.Persistence.Constants;
using DigiDent.Shared.Abstractions.Factories;
using DigiDent.Shared.Kernel.ValueObjects.Pagination;
using Microsoft.Data.SqlClient;

namespace DigiDent.InventoryManagement.Persistence.Requests;

public class RequestsQueriesRepository: IRequestsQueriesRepository
{
    private readonly IDbConnectionFactory<SqlConnection> _connectionFactory;

    public RequestsQueriesRepository(
        IDbConnectionFactory<SqlConnection> connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<PaginatedResponse<RequestSummary>> GetAllAsync(
        IPaginationOptions pagination, CancellationToken cancellationToken)
    {
        const string schema = ConfigurationConstants.InventoryManagementSchema;
        const string sql = $@"
            SELECT r.[Id], r.[RequestedItemId], i.[Name] AS [RequestedItemName], 
                i.[Category] AS [ItemCategory], r.[RequestedQuantity], 
                r.[Status], r.[RequesterId], e.[Name] AS [RequesterName], 
                r.[DateOfRequest], r.[Remarks]
            FROM {schema}.[Requests] r 
                JOIN {schema}.[Employees] e ON r.[RequesterId] = e.[Id]
                JOIN {schema}.[Items] i ON r.[RequestedItemId] = i.[Id]
            ORDER BY r.[DateOfRequest] @SortDirection
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
        
        await using var connection = _connectionFactory.CreateOpenConnection();
        
        IReadOnlyCollection<RequestSummary> requests = (await connection
                .QueryAsync<RequestSummary>(
                    sql, new
                    {
                        SortDirection = pagination.SortOrder,
                        Offset = pagination.PageSize * (pagination.PageNumber - 1),
                        PageSize = pagination.PageSize
                    }))
            .Select(request => request with
            {
                ItemCategory = Enum
                    .Parse<ItemCategory>(request.ItemCategory)
                    .ToString(),
                
                Status = Enum
                    .Parse<RequestStatus>(request.Status)
                    .ToString()
            })
            .Where(request => request.Contains(pagination.SearchTerm))
            .AsList()
            .AsReadOnly();
        
        return new PaginatedResponse<RequestSummary>(
            DataCollection: requests,
            TotalCount: requests.Count);
    }
}