using Dapper;
using DigiDent.InventoryManagement.Domain.Actions.ReadModels;
using DigiDent.InventoryManagement.Domain.Actions.Repositories;
using DigiDent.InventoryManagement.Domain.Employees.ReadModels;
using DigiDent.InventoryManagement.Domain.Items.ReadModels;
using DigiDent.InventoryManagement.Persistence.Constants;
using DigiDent.Shared.Infrastructure.Persistence.Factories;
using DigiDent.Shared.Kernel.Pagination;

namespace DigiDent.InventoryManagement.Persistence.Actions;

public class InventoryActionsQueriesRepository: IInventoryActionsQueriesRepository
{
    private const string Schema = ConfigurationConstants.InventoryManagementSchema;
    private readonly SqlConnectionFactory _connectionFactory;

    public InventoryActionsQueriesRepository(
        SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<PaginatedResponse<ActionSummary>> GetAllAsync(
        IPaginationOptions pagination, CancellationToken cancellationToken)
    {
        const string query = 
            $"""
             SELECT
                a.[Id] AS [{nameof(ActionSummary.Id)}],
                a.[Type] AS [{nameof(ActionSummary.Type)}],
                a.[Quantity] AS [{nameof(ActionSummary.Quantity)}],
                a.[Date] AS [{nameof(ActionSummary.Date)},
                e.[Id] AS [{nameof(EmployeeSummary.Id)}],
                e.[Name] AS [{nameof(EmployeeSummary.FullName)}],
                e.[Email] AS [{nameof(EmployeeSummary.Email)}],
                e.[PhoneNumber] AS [{nameof(EmployeeSummary.PhoneNumber)}],
                e.[Position] AS [{nameof(EmployeeSummary.Position)}],
                i.[Id] AS [{nameof(InventoryItemSummary.Id)}],
                i.[Name] AS [{nameof(InventoryItemSummary.Name)}],
                i.[Quantity] AS [{nameof(InventoryItemSummary.Quantity)}],
                i.[Category] AS [{nameof(InventoryItemSummary.Category)}]
             FROM {Schema}.[Actions] a 
                 LEFT JOIN {Schema}.Employees e ON a.EmployeeId = e.Id
                 LEFT JOIN {Schema}.InventoryItems i ON a.InventoryItemId = i.Id
             ORDER BY a.[Date] @SortDirection
             WHERE a.[Id] > @Cursor
             LIMIT @PageSize
             """;
        
        
        await using var connection = _connectionFactory.CreateOpenConnection();
        IReadOnlyCollection<ActionSummary> actions = (await connection
                .QueryAsync<ActionSummary, EmployeeSummary, InventoryItemSummary, ActionSummary>(
                    sql: query, 
                    map: (action, employee, inventoryItem) => action with
                    {
                        ActionPerformer = employee,
                        InventoryItem = inventoryItem
                    }, 
                    param: new
                    {
                        SortDirection = pagination.SortOrder,
                        Cursor = pagination.PageSize * (pagination.PageNumber - 1),
                        PageSize = pagination.PageSize
                    },
                    splitOn: $"{nameof(EmployeeSummary.Id)}, {nameof(InventoryItemSummary.Id)}"))
            .Filter(pagination.SearchTerm)
            .AsList()
            .AsReadOnly();
        
        return new PaginatedResponse<ActionSummary>(
            DataCollection: actions,
            TotalCount: actions.Count);
    }
}