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
                re.[Id] AS [{nameof(EmployeeSummary.Id)}],
                re.[Name] AS [{nameof(EmployeeSummary.FullName)}],
                re.[Email] AS [{nameof(EmployeeSummary.Email)}],
                re.[PhoneNumber] AS [{nameof(EmployeeSummary.PhoneNumber)}],
                re.[Position] AS [{nameof(EmployeeSummary.Position)}],
                a.[Id] AS [{nameof(ActionSummary.Id)}],
                a.[Type] AS [{nameof(ActionSummary.Type)}],
                a.[Quantity] AS [{nameof(ActionSummary.Quantity)}],
                a.[Date] AS [{nameof(ActionSummary.Date)}],
                ae.[Id] AS [{nameof(EmployeeSummary.Id)}],
                ae.[Name] AS [{nameof(EmployeeSummary.FullName)}],
                ae.[Email] AS [{nameof(EmployeeSummary.Email)}],
                ae.[PhoneNumber] AS [{nameof(EmployeeSummary.PhoneNumber)}],
                ae.[Position] AS [{nameof(EmployeeSummary.Position)}]
             FROM {Schema}.[Items] i
                LEFT JOIN {Schema}.[Requests] r ON i.[Id] = r.[RequestedItemId]
                LEFT JOIN {Schema}.[Employees] re ON r.[RequesterId] = re.[Id]
                LEFT JOIN {Schema}.[Actions] a ON i.[Id] = a.[InventoryItemId]
                LEFT JOIN {Schema}.[Employees] ae ON a.[ActionPerformerId] = ae.[Id]
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
        IPaginationData pagination, CancellationToken cancellationToken)
    {
        const string query = 
            $"""
             SELECT TOP (@PageSize)
                [Id] AS [{nameof(InventoryItemSummary.Id)}],
                [Name] AS [{nameof(InventoryItemSummary.Name)}],
                [Quantity] AS [{nameof(InventoryItemSummary.Quantity)}],
                [Category] AS [{nameof(InventoryItemSummary.Category)}]
             FROM {Schema}.[Items] 
             WHERE [Id] > @Cursor
             """;

        await using var connection = _connectionFactory.CreateOpenConnection();

        IReadOnlyCollection<InventoryItemSummary> items = (await connection
                .QueryAsync<InventoryItemSummary>(
                    sql: query,
                    param: new
                    {
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