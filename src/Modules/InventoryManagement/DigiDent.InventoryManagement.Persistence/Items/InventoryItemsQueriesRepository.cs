using Dapper;
using DigiDent.InventoryManagement.Domain.Actions.ReadModels;
using DigiDent.InventoryManagement.Domain.Employees.ReadModels;
using DigiDent.InventoryManagement.Domain.Items.ReadModels;
using DigiDent.InventoryManagement.Domain.Items.Repositories;
using DigiDent.InventoryManagement.Domain.Requests.ReadModels;
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
        int id, CancellationToken cancellationToken)
    {
        await using var connection = _connectionFactory.CreateOpenConnection();
        if (!await SharedRepository.ExistsAsync(tableName: "Items", id, connection))
        {
            return null;
        }
        
        const string query = 
            $"""
             SELECT 
                i.[Id] AS [{nameof(InventoryItemSummary.Id)}],
                i.[Name] AS [{nameof(InventoryItemSummary.Name)}],
                i.[Quantity] AS [{nameof(InventoryItemSummary.Quantity)}],
                i.[Category] AS [{nameof(InventoryItemSummary.Category)}],
                r.[Id] AS [{nameof(RequestSummary.Id)}],
                r.[Status] AS [{nameof(RequestSummary.Status)}],
                r.[RequestedQuantity] AS [{nameof(RequestSummary.RequestedQuantity)}],
                r.[DateOfRequest] AS [{nameof(RequestSummary.DateOfRequest)}],
                r.[Remarks] AS [{nameof(RequestSummary.Remarks)}],
                a.[Id] AS [{nameof(ActionSummary.Id)}],
                a.[Type] AS [{nameof(ActionSummary.Type)}],
                a.[Quantity] AS [{nameof(ActionSummary.Quantity)}],
                a.[Date] AS [{nameof(ActionSummary.Date)}],
                e.[Id] AS [{nameof(EmployeeSummary.Id)}],
                e.[Name] AS [{nameof(EmployeeSummary.FullName)}],
                e.[Email] AS [{nameof(EmployeeSummary.Email)}],
                e.[PhoneNumber] AS [{nameof(EmployeeSummary.PhoneNumber)}],
                e.[Position] AS [{nameof(EmployeeSummary.Position)}]
             FROM {Schema}.[Items] i
                LEFT JOIN {Schema}.[Requests] r ON i.[Id] = r.[ItemId]
                LEFT JOIN {Schema}.[Employees] e ON r.[RequesterId] = e.[Id]
                LEFT JOIN {Schema}.[Actions] a ON i.[Id] = a.[ItemId]
                LEFT JOIN {Schema}.[Employees] e ON a.[ActionPerformerId] = e.[Id]
             WHERE i.[Id] = @Id
             """;
        
        InventoryItemDetails item = (await connection.QueryAsync
            <InventoryItemDetails, RequestSummary?, EmployeeSummary?, ActionSummary?, EmployeeSummary?, InventoryItemDetails>(
                sql: query, 
                map: (item, request, requesterEmployee, action, performerEmployee) =>
                {
                    if (request is not null && requesterEmployee is not null)
                    {
                        request.Requester = requesterEmployee;
                        item.Requests.Add(request);
                    }
                    if (action is not null && performerEmployee is not null)
                    {
                        action.ActionPerformer = performerEmployee;
                        item.InventoryActions.Add(action);
                    }
                    return item;
                },
                param: new { Id = id },
                splitOn: $"{nameof(RequestSummary.Id)}" + 
                         $", {nameof(EmployeeSummary.Id)}" + 
                         $", {nameof(ActionSummary.Id)}" + 
                         $", {nameof(EmployeeSummary.Id)}"))
            .First();
        
        return item;
    }

    public async Task<PaginatedResponse<InventoryItemSummary>> GetAllAsync(
        IPaginationOptions pagination, CancellationToken cancellationToken)
    {
        const string query = 
            $"""
             SELECT 
                [Id] AS [{nameof(InventoryItemSummary.Id)}],
                [Name] AS [{nameof(InventoryItemSummary.Name)}],
                [Quantity] AS [{nameof(InventoryItemSummary.Quantity)}],
                [Category] AS [{nameof(InventoryItemSummary.Category)}]
             FROM {Schema}.[Items] 
             WHERE [Id] > @Cursor
             LIMIT @PageSize
            """;

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
            .Filter(pagination.SearchTerm)
            .SortBy(pagination.SortByColumn, pagination.SortOrder)
            .AsList()
            .AsReadOnly();

        return new PaginatedResponse<InventoryItemSummary>(
            DataCollection: items,
            TotalCount: items.Count);
    }
}