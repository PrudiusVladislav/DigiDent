using Dapper;
using DigiDent.InventoryManagement.Domain.Actions;
using DigiDent.InventoryManagement.Domain.Actions.ReadModels;
using DigiDent.InventoryManagement.Domain.Actions.ValueObjects;
using DigiDent.InventoryManagement.Domain.Employees;
using DigiDent.InventoryManagement.Domain.Employees.ReadModels;
using DigiDent.InventoryManagement.Domain.Employees.ValueObjects;
using DigiDent.InventoryManagement.Domain.Items;
using DigiDent.InventoryManagement.Domain.Items.ReadModels;
using DigiDent.InventoryManagement.Domain.Items.Repositories;
using DigiDent.InventoryManagement.Domain.Items.ValueObjects;
using DigiDent.InventoryManagement.Domain.Requests;
using DigiDent.InventoryManagement.Domain.Requests.ReadModels;
using DigiDent.InventoryManagement.Domain.Requests.ValueObjects;
using DigiDent.InventoryManagement.Persistence.Constants;
using DigiDent.InventoryManagement.Persistence.Shared;
using DigiDent.Shared.Infrastructure.Persistence.Factories;
using DigiDent.Shared.Kernel.Pagination;

namespace DigiDent.InventoryManagement.Persistence.Items;

public class InventoryItemsQueriesRepository: IInventoryItemsQueriesRepository
{
    private const string Schema = ConfigurationConstants.InventoryManagementSchema;
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
        
        const string query = $@"
            SELECT 
                i.[{nameof(InventoryItem.Id)}] AS [{nameof(InventoryItemSummary.Id)}],
                i.[{nameof(InventoryItem.Name)}] AS [{nameof(InventoryItemSummary.Name)}],
                i.[{nameof(InventoryItem.Quantity)}] AS [{nameof(InventoryItemSummary.Quantity)}],
                i.[{nameof(InventoryItem.Category)}] AS [{nameof(InventoryItemSummary.Category)}],
                r.[{nameof(Request.Id)}] AS [{nameof(RequestSummary.Id)}],
                r.[{nameof(Request.Status)}] AS [{nameof(RequestSummary.Status)}],
                r.[{nameof(Request.RequestedQuantity)}] AS [{nameof(RequestSummary.RequestedQuantity)}],
                r.[{nameof(Request.DateOfRequest)}] AS [{nameof(RequestSummary.DateOfRequest)}],
                r.[{nameof(Request.Remarks)}] AS [{nameof(RequestSummary.Remarks)}],
                a.[{nameof(InventoryAction.Id)}] AS [{nameof(ActionSummary.Id)}],
                a.[{nameof(InventoryAction.Type)}] AS [{nameof(ActionSummary.Type)}],
                a.[{nameof(InventoryAction.Quantity)}] AS [{nameof(ActionSummary.Quantity)}],
                a.[{nameof(InventoryAction.Date)}] AS [{nameof(ActionSummary.Date)}],
                e.[{nameof(Employee.Id)}] AS [{nameof(EmployeeSummary.Id)}],
                e.[{nameof(Employee.Name)}] AS [{nameof(EmployeeSummary.FullName)}],
                e.[{nameof(Employee.Email)}] AS [{nameof(EmployeeSummary.Email)}],
                e.[{nameof(Employee.PhoneNumber)}] AS [{nameof(EmployeeSummary.PhoneNumber)}],
                e.[{nameof(Employee.Position)}] AS [{nameof(EmployeeSummary.Position)}]
            FROM {Schema}.[Items] i
                LEFT JOIN {Schema}.[Requests] r ON i.[Id] = r.[ItemId]
                LEFT JOIN {Schema}.[Employees] e ON r.[RequesterId] = e.[Id]
                LEFT JOIN {Schema}.[Actions] a ON i.[Id] = a.[ItemId]
                LEFT JOIN {Schema}.[Employees] e ON a.[ActionPerformerId] = e.[Id]
            WHERE i.[Id] = @Id";
        
        await using var connection = _connectionFactory.CreateOpenConnection();
        
        InventoryItemDetails item = (await connection.QueryAsync
            <InventoryItemDetails, RequestSummary?, EmployeeSummary?, ActionSummary?, EmployeeSummary?, InventoryItemDetails>(
                sql: query, 
                map: (item, request, requesterEmployee, action, performerEmployee) =>
                {
                    if (request is not null && requesterEmployee is not null)
                    {
                        request.Requester = requesterEmployee with
                        {
                            Position = requesterEmployee.Position.ToEnumName<Position>()
                        };
                        item.Requests.Add(request with
                        {
                            Status = request.Status.ToEnumName<RequestStatus>()
                        });
                    }
                    
                    if (action is not null && performerEmployee is not null)
                    {
                        action.ActionPerformer = performerEmployee with
                        {
                            Position = performerEmployee.Position.ToEnumName<Position>()
                        };
                        item.InventoryActions.Add(action with 
                        {
                            Type = action.Type.ToEnumName<ActionType>()
                        });
                    }
                    
                    return item with { Category = item.Category.ToEnumName<ItemCategory>() };
                },
                param: new { Id = id.Value },
                splitOn: $"{nameof(RequestSummary.Id)}" + 
                         $", {nameof(EmployeeSummary.Id)}" + 
                         $", {nameof(ActionSummary.Id)}" + 
                         $", {nameof(EmployeeSummary.Id)}"))
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
        const string query = $@"
            SELECT [Id], [Name], [Quantity], [Category]
            FROM {Schema}.[Items] 
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
                Category = item.Category.ToEnumName<ItemCategory>()
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