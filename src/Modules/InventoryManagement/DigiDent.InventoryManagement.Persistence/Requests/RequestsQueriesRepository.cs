using Dapper;
using DigiDent.InventoryManagement.Domain.Employees;
using DigiDent.InventoryManagement.Domain.Employees.ReadModels;
using DigiDent.InventoryManagement.Domain.Items.ReadModels;
using DigiDent.InventoryManagement.Domain.Items.ValueObjects;
using DigiDent.InventoryManagement.Domain.Requests;
using DigiDent.InventoryManagement.Domain.Requests.ReadModels;
using DigiDent.InventoryManagement.Domain.Requests.Repositories;
using DigiDent.InventoryManagement.Domain.Requests.ValueObjects;
using DigiDent.InventoryManagement.Persistence.Constants;
using DigiDent.Shared.Infrastructure.Persistence.Factories;
using DigiDent.Shared.Kernel.Extensions;
using DigiDent.Shared.Kernel.Pagination;

namespace DigiDent.InventoryManagement.Persistence.Requests;

public class RequestsQueriesRepository: IRequestsQueriesRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public RequestsQueriesRepository(
        SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<PaginatedResponse<RequestSummary>> GetAllAsync(
        IPaginationOptions pagination, CancellationToken cancellationToken)
    {
        const string schema = ConfigurationConstants.InventoryManagementSchema;
        const string query = $@"
            SELECT 
                r.[Id] as RequestId,
                r.[Status] as RequestStatus,
                r.[RequestedQuantity] as RequestedQuantity,
                r.[DateOfRequest] as DateOfRequest,
                r.[Remarks] as RequestRemarks,
                i.[Id] as ItemId,
                i.[Name] as ItemName,
                i.[Quantity] as ItemQuantity,
                i.[Category] as ItemCategory,
                e.[Id] as EmployeeId,
                e.[FullName] as EmployeeFullName
            FROM {schema}.[Requests] r 
                JOIN {schema}.[Items] i ON r.[RequestedItemId] = i.[Id]
                JOIN {schema}.[Employees] e ON r.[RequesterId] = e.[Id]
            ORDER BY r.[DateOfRequest] @SortDirection
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
        
        await using var connection = _connectionFactory.CreateOpenConnection();
        
        IReadOnlyCollection<RequestSummary> requests = (await connection
                .QueryAsync<RequestSummary, InventoryItemSummary, EmployeeSummary, RequestSummary>(
                    sql: query, 
                    map: (request, inventoryItem, requester) => request with
                    {
                        Status = Enum.Parse<RequestStatus>(request.Status).ToString(),
                        RequestedItem = inventoryItem with
                        {
                            Category = Enum
                                .Parse<ItemCategory>(inventoryItem.Category)
                                .ToString()
                        },
                        Requester = requester
                    },
                    param: new
                    {
                        SortDirection = pagination.SortOrder,
                        Offset = pagination.PageSize * (pagination.PageNumber - 1),
                        PageSize = pagination.PageSize
                    }))
            .Filter(pagination.SearchTerm)
            .AsList()
            .AsReadOnly();
        
        return new PaginatedResponse<RequestSummary>(
            DataCollection: requests,
            TotalCount: requests.Count);
    }
}