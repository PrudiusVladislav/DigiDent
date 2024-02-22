using Dapper;
using DigiDent.InventoryManagement.Domain.Actions;
using DigiDent.InventoryManagement.Domain.Actions.ReadModels;
using DigiDent.InventoryManagement.Domain.Actions.ValueObjects;
using DigiDent.InventoryManagement.Domain.Items.ValueObjects;
using DigiDent.InventoryManagement.Persistence.Constants;
using DigiDent.InventoryManagement.Persistence.Shared;
using DigiDent.Shared.Kernel.ValueObjects.Pagination;

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
        const string sql = $@"
            SELECT a.[Id], a.[Type], a.[Quantity], a.[Date], 
            e.[Id] AS [EmployeeId], e.[Name] AS [EmployeeName], 
            i.[Id] AS [ItemId], i.[Name] AS [ItemName], i.[Category] AS [ItemCategory]
            FROM {schema}.[Actions] a 
                JOIN {schema}.[Employees] e ON a.[ActionPerformerId] = e.[Id]
                JOIN {schema}.[Items] i ON a.[InventoryItemId] = i.[Id]
            ORDER BY a.[Date] @SortDirection
            WHERE a.[Id] > @Cursor
            LIMIT @PageSize";
        
        await using var connection = _connectionFactory.CreateOpenConnection();
        IReadOnlyCollection<ActionSummary> actions = (await connection
                .QueryAsync<ActionSummary>(
                    sql, new
                    { 
                        SortDirection = pagination.SortOrder,
                        Cursor = pagination.PageSize * (pagination.PageNumber - 1),
                        PageSize = pagination.PageSize
                    }))
            .Select(action => action with
            {
                Type = Enum
                    .Parse<ActionType>(action.Type)
                    .ToString(),
                
                ItemCategory = Enum
                    .Parse<ItemCategory>(action.ItemCategory)
                    .ToString()
            })
            .Where(action => action.Contains(pagination.SearchTerm))
            .AsList()
            .AsReadOnly();
        
        return new PaginatedResponse<ActionSummary>(
            DataCollection: actions,
            TotalCount: actions.Count);
    }
}