using Dapper;
using DigiDent.InventoryManagement.Domain.Employees;
using DigiDent.InventoryManagement.Domain.Employees.ReadModels;
using DigiDent.InventoryManagement.Domain.Items;
using DigiDent.InventoryManagement.Domain.Items.ReadModels;
using DigiDent.InventoryManagement.Domain.Items.ValueObjects;
using DigiDent.InventoryManagement.Domain.Requests;
using DigiDent.InventoryManagement.Domain.Requests.ReadModels;
using DigiDent.InventoryManagement.Domain.Requests.Repositories;
using DigiDent.InventoryManagement.Domain.Requests.ValueObjects;
using DigiDent.InventoryManagement.Persistence.Constants;
using DigiDent.Shared.Infrastructure.Persistence.Factories;
using DigiDent.Shared.Kernel.Pagination;

namespace DigiDent.InventoryManagement.Persistence.Requests;

public class RequestsQueriesRepository: IRequestsQueriesRepository
{
    private const string Schema = ConfigurationConstants.InventoryManagementSchema;
    private readonly SqlConnectionFactory _connectionFactory;

    public RequestsQueriesRepository(
        SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<PaginatedResponse<RequestSummary>> GetAllAsync(
        IPaginationOptions pagination, CancellationToken cancellationToken)
    {
        const string query = 
            $"""
             SELECT
                r.[{nameof(Request.Id)}] AS [{nameof(RequestSummary.Id)}],
                r.[{nameof(Request.Status)}] AS [{nameof(RequestSummary.Status)}],
                r.[{nameof(Request.RequestedQuantity)}] AS [{nameof(RequestSummary.RequestedQuantity)}],
                r.[{nameof(Request.DateOfRequest)}] AS [{nameof(RequestSummary.DateOfRequest)}],
                r.[{nameof(Request.Remarks)}] AS [{nameof(RequestSummary.Remarks)}],
                i.[{nameof(InventoryItem.Id)}] AS [{nameof(InventoryItemSummary.Id)}],
                i.[{nameof(InventoryItem.Name)}] AS [{nameof(InventoryItemSummary.Name)}],
                i.[{nameof(InventoryItem.Quantity)}] AS [{nameof(InventoryItemSummary.Quantity)}],
                i.[{nameof(InventoryItem.Category)}] AS [{nameof(InventoryItemSummary.Category)}],
                e.[{nameof(Employee.Id)}] AS [{nameof(EmployeeSummary.Id)}],
                e.[{nameof(Employee.Name)}] AS [{nameof(EmployeeSummary.FullName)}]
             FROM {Schema}.[Requests] r
                  LEFT JOIN {Schema}.[Items] i ON r.[RequestedItemId] = i.[Id]
                  LEFT JOIN {Schema}.[Employees] e ON r.[RequesterId] = e.[Id]
             ORDER BY r.[{nameof(Request.DateOfRequest)}] @SortDirection
             OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY
             """;
        
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
                    },
                    splitOn: $"{nameof(InventoryItemSummary.Id)}, {nameof(EmployeeSummary.Id)}"))
            .Filter(pagination.SearchTerm)
            .AsList()
            .AsReadOnly();
        
        return new PaginatedResponse<RequestSummary>(
            DataCollection: requests,
            TotalCount: requests.Count);
    }
}