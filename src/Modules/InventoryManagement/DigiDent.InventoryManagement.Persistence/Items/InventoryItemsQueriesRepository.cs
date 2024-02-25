using Dapper;
using DigiDent.InventoryManagement.Domain.Actions.ReadModels;
using DigiDent.InventoryManagement.Domain.Employees;
using DigiDent.InventoryManagement.Domain.Employees.ReadModels;
using DigiDent.InventoryManagement.Domain.Items;
using DigiDent.InventoryManagement.Domain.Items.ReadModels;
using DigiDent.InventoryManagement.Domain.Items.Repositories;
using DigiDent.InventoryManagement.Domain.Items.ValueObjects;
using DigiDent.InventoryManagement.Domain.Requests.ReadModels;
using DigiDent.InventoryManagement.Persistence.Constants;
using DigiDent.Shared.Infrastructure.Persistence.Factories;
using DigiDent.Shared.Kernel.Pagination;

namespace DigiDent.InventoryManagement.Persistence.Items;

public class InventoryItemsQueriesRepository: IInventoryItemsQueriesRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public InventoryItemsQueriesRepository(
        SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<InventoryItemDetails?> GetByIdAsync(
        InventoryItemId id, CancellationToken cancellationToken)
    {
        if (!await ExistsAsync(id))
        {
            return null;
        }
        
        const string schema = ConfigurationConstants.InventoryManagementSchema;
        const string query = $@"
            SELECT 
                i.[Id] AS InventoryItemId,
                i.[Name] AS InventoryItemName,
                i.[Quantity] AS InventoryItemQuantity,
                i.[Category] AS InventoryItemCategory,
                i.[Remarks] AS InventoryItemRemarks,
                r.[Id] AS RequestId,
                r.[Status] AS RequestStatus,
                r.[RequestedQuantity] AS RequestedQuantity,
                r.[DateOfRequest] AS RequestDateOfRequest,
                r.[Remarks] AS RequestRemarks,
                a.[Id] AS ActionId,
                a.[Type] AS ActionType,
                a.[Quantity] AS ActionQuantity,
                a.[Date] AS ActionDate,
                e.[Id] AS EmployeeId,
                e.[FullName] AS EmployeeFullName
            FROM {schema}.[Items] i
                LEFT JOIN {schema}.[Requests] r ON i.[Id] = r.[ItemId]
                LEFT JOIN {schema}.[Actions] a ON i.[Id] = a.[ItemId]
                LEFT JOIN {schema}.[Employees] e ON a.[EmployeeId] = e.[Id]
            WHERE i.[Id] = @Id";
        
        await using var connection = _connectionFactory.CreateOpenConnection();
        
        InventoryItemDetails item = (await connection.QueryAsync<
            InventoryItemDetails, RequestSummary, ActionSummary, EmployeeSummary, InventoryItemDetails>(
                sql: query, 
                map: (item, request, action, employee) =>
                {
                    InventoryItemSummary itemSummary = new()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Quantity = item.Quantity,
                        Category = Enum
                            .Parse<ItemCategory>(item.Category)
                            .ToString()
                    };
                    
                    item.InventoryActions.Add(action);
                    action.ActionPerformer = employee;
                    action.InventoryItem = itemSummary;
                    
                    item.Requests.Add(request);
                    request.RequestedItem = itemSummary;
                    request.Requester = employee;
                    
                    return item;
                },
                param: new { Id = id.Value },
                splitOn: "RequestId, ActionId, EmployeeId"))
            .First();
        
        return item;
    }
    
    private async Task<bool> ExistsAsync(InventoryItemId id)
    {
        const string schema = ConfigurationConstants.InventoryManagementSchema;
        const string query = $@"
            SELECT 
                COUNT(*)
            FROM {schema}.[Items] 
            WHERE [Id] = @Id";
        
        await using var connection = _connectionFactory.CreateOpenConnection();
        var result = await connection.ExecuteScalarAsync<int>(
            sql: query, 
            param: new { Id = id.Value });

        return result > 0;
    }

    public async Task<PaginatedResponse<InventoryItemSummary>> GetAllAsync(
        IPaginationOptions pagination, CancellationToken cancellationToken)
    {
        const string schema = ConfigurationConstants.InventoryManagementSchema;
        const string query = $@"
            SELECT [Id], [Name], [Quantity], [Category]
            FROM {schema}.[Items] 
            WHERE [Id] > @Cursor
            LIMIT @PageSize";

        await using var connection = _connectionFactory.CreateOpenConnection();

        IReadOnlyCollection<InventoryItemSummary> items = (await connection
                .QueryAsync<InventoryItemSummary>(
                    sql: query,
                    param: new
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
            .Filter(pagination.SearchTerm)
            .SortBy(pagination.SortByColumn, pagination.SortOrder)
            .AsList()
            .AsReadOnly();

        return new PaginatedResponse<InventoryItemSummary>(
            DataCollection: items,
            TotalCount: items.Count);
    }
}