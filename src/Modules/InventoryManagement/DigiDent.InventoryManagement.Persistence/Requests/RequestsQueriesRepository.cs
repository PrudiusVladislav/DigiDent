using Dapper;
using DigiDent.InventoryManagement.Domain.Employees.ReadModels;
using DigiDent.InventoryManagement.Domain.Items.ReadModels;
using DigiDent.InventoryManagement.Domain.Requests.ReadModels;
using DigiDent.InventoryManagement.Domain.Requests.Repositories;
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
        IPaginationData pagination, CancellationToken cancellationToken)
    { 
        string query = 
            $"""
             SELECT
                r.[Id] AS [{nameof(RequestSummary.Id)}],
                r.[Status] AS [{nameof(RequestSummary.Status)}],
                r.[RequestedQuantity] AS [{nameof(RequestSummary.RequestedQuantity)}],
                r.[DateOfRequest] AS [{nameof(RequestSummary.DateOfRequest)}],
                r.[Remarks] AS [{nameof(RequestSummary.Remarks)}],
                i.[Id] AS [{nameof(InventoryItemSummary.Id)}],
                i.[Name] AS [{nameof(InventoryItemSummary.Name)}],
                i.[Quantity] AS [{nameof(InventoryItemSummary.Quantity)}],
                i.[Category] AS [{nameof(InventoryItemSummary.Category)}],
                e.[Id] AS [{nameof(EmployeeSummary.Id)}],
                e.[Name] AS [{nameof(EmployeeSummary.FullName)}],
                e.[Email] AS [{nameof(EmployeeSummary.Email)}],
                e.[PhoneNumber] AS [{nameof(EmployeeSummary.PhoneNumber)}],
                e.[Position] AS [{nameof(EmployeeSummary.Position)}]
             FROM {Schema}.[Requests] r
                  LEFT JOIN {Schema}.[Items] i ON r.[RequestedItemId] = i.[Id]
                  LEFT JOIN {Schema}.[Employees] e ON r.[RequesterId] = e.[Id]
             ORDER BY r.[DateOfRequest] {pagination.SortOrder.ToString()}
             OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY
             """;
        
        await using var connection = _connectionFactory.CreateOpenConnection();
        
        IReadOnlyCollection<RequestSummary> requests = (await connection
                .QueryAsync<RequestSummary, InventoryItemSummary, EmployeeSummary, RequestSummary>(
                    sql: query, 
                    map: (request, inventoryItem, requester) => request with
                    {
                        RequestedItem = inventoryItem,
                        Requester = requester
                    },
                    param: new
                    {
                        Offset = pagination.PageSize * (pagination.PageNumber - 1),
                        PageSize = pagination.PageSize
                    },
                    splitOn: $"{nameof(InventoryItemSummary.Id)}" + 
                             $", {nameof(EmployeeSummary.Id)}"))
            .Filter(pagination.SearchTerm)
            .AsList()
            .AsReadOnly();
        
        return new PaginatedResponse<RequestSummary>(
            DataCollection: requests,
            TotalCount: requests.Count);
    }
}