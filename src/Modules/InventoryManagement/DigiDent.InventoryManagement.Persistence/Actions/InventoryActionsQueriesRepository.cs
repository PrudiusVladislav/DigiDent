using Dapper;
using DigiDent.InventoryManagement.Domain.Actions;
using DigiDent.InventoryManagement.Domain.Actions.ReadModels;
using DigiDent.InventoryManagement.Domain.Actions.Repositories;
using DigiDent.InventoryManagement.Domain.Actions.ValueObjects;
using DigiDent.InventoryManagement.Domain.Employees;
using DigiDent.InventoryManagement.Domain.Employees.ReadModels;
using DigiDent.InventoryManagement.Domain.Items.ReadModels;
using DigiDent.InventoryManagement.Domain.Items.ValueObjects;
using DigiDent.InventoryManagement.Persistence.Constants;
using DigiDent.Shared.Infrastructure.Persistence.Factories;
using DigiDent.Shared.Kernel.Pagination;

namespace DigiDent.InventoryManagement.Persistence.Actions;

public class InventoryActionsQueriesRepository: IInventoryActionsQueriesRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public InventoryActionsQueriesRepository(
        SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<PaginatedResponse<ActionSummary>> GetAllAsync(
        IPaginationOptions pagination, CancellationToken cancellationToken)
    {
        const string schema = ConfigurationConstants.InventoryManagementSchema;
        const string query = $@"
            SELECT
                a.[Id] AS ActionId,
                a.[Type] AS ActionType,
                a.[Quantity] AS ActionQuantity,
                a.[Date] AS ActionDate,
                e.[Id] AS EmployeeId,
                e.[FullName] AS EmployeeFullName,
                i.[Id] AS InventoryItemId,
                i.[Name] AS InventoryItemName,
                i.[Quantity] AS InventoryItemQuantity,
                i.[Category] AS InventoryItemCategory
            FROM 
                {schema}.[Actions] a
                INNER JOIN {schema}.Employees e ON a.EmployeeId = e.Id
                INNER JOIN {schema}.InventoryItems i ON a.InventoryItemId = i.Id
            ORDER BY a.[Date] @SortDirection
            WHERE a.[Id] > @Cursor
            LIMIT @PageSize";
        
        await using var connection = _connectionFactory.CreateOpenConnection();
        IReadOnlyCollection<ActionSummary> actions = (await connection
                .QueryAsync<ActionSummary, EmployeeSummary, InventoryItemSummary, ActionSummary>(
                    sql: query, 
                    map: (action, employee, inventoryItem) => action with
                    {
                        Type = Enum.Parse<ActionType>(action.Type).ToString(),
                        ActionPerformer = employee,
                        InventoryItem = inventoryItem with 
                        {
                            Category = Enum
                                .Parse<ItemCategory>(inventoryItem.Category)
                                .ToString()
                        }
                    }, 
                    splitOn: "EmployeeId, InventoryItemId",
                    param: new
                    {
                        SortDirection = pagination.SortOrder,
                        Cursor = pagination.PageSize * (pagination.PageNumber - 1),
                        PageSize = pagination.PageSize
                    }))
            .Filter(pagination.SearchTerm)
            .AsList()
            .AsReadOnly();
        
        return new PaginatedResponse<ActionSummary>(
            DataCollection: actions,
            TotalCount: actions.Count);
    }
}