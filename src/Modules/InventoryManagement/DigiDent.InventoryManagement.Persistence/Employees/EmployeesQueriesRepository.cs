using Dapper;
using DigiDent.InventoryManagement.Domain.Actions.ReadModels;
using DigiDent.InventoryManagement.Domain.Employees.ReadModels;
using DigiDent.InventoryManagement.Domain.Employees.Repositories;
using DigiDent.InventoryManagement.Domain.Items.ReadModels;
using DigiDent.InventoryManagement.Domain.Requests.ReadModels;
using DigiDent.InventoryManagement.Persistence.Constants;
using DigiDent.InventoryManagement.Persistence.Shared;
using DigiDent.Shared.Infrastructure.Persistence.Factories;
using DigiDent.Shared.Kernel.Pagination;

namespace DigiDent.InventoryManagement.Persistence.Employees;

public class EmployeesQueriesRepository: IEmployeesQueriesRepository
{
    private const string Schema = ConfigurationConstants.InventoryManagementSchema;
    private readonly SqlConnectionFactory _connectionFactory;

    public EmployeesQueriesRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<EmployeeDetails?> GetByIdAsync(
        Guid id, CancellationToken cancellationToken)
    {
        await using var connection = _connectionFactory.CreateConnection();
        if (!await SharedRepository.ExistsAsync(tableName: "Employees", id, connection))
        {
            return null;
        }
        
        const string query = 
            $"""
             SELECT 
                e.[Id] AS [{nameof(EmployeeDetails.Id)}],
                e.[Name] AS [{nameof(EmployeeDetails.FullName)}],
                e.[Email] AS [{nameof(EmployeeDetails.Email)}],
                e.[PhoneNumber] AS [{nameof(EmployeeDetails.PhoneNumber)}],
                e.[Position] AS [{nameof(EmployeeDetails.Position)}],
                r.[Id] AS [{nameof(RequestSummary.Id)}],
                r.[Status] AS [{nameof(RequestSummary.Status)}],
                r.[RequestedQuantity] AS [{nameof(RequestSummary.RequestedQuantity)}],
                r.[DateOfRequest] AS [{nameof(RequestSummary.DateOfRequest)}],
                r.[Remarks] AS [{nameof(RequestSummary.Remarks)},
                ri.[Id] AS [{nameof(InventoryItemSummary.Id)}],
                ri.[Name] AS [{nameof(InventoryItemSummary.Name)}],
                ri.[Quantity] AS [{nameof(InventoryItemSummary.Quantity)}],
                ri.[Category] AS [{nameof(InventoryItemSummary.Category)},
                a.[Id] AS [{nameof(ActionSummary.Id)}],
                a.[Type] AS [{nameof(ActionSummary.Type)}],
                a.[Quantity] AS [{nameof(ActionSummary.Quantity)}],
                a.[Date] AS [{nameof(ActionSummary.Date)}],
                ai.[Id] AS [{nameof(InventoryItemSummary.Id)}],
                ai.[Name] AS [{nameof(InventoryItemSummary.Name)}],
                ai.[Quantity] AS [{nameof(InventoryItemSummary.Quantity)}],
                ai.[Category] AS [{nameof(InventoryItemSummary.Category)}]
             FROM {Schema}.[Employees] e
                 LEFT JOIN {Schema}.[Requests] r ON e.[Id] = r.[RequesterId]
                 LEFT JOIN {Schema}.[Items] ri ON r.[ItemId] = ri.[Id]
                 LEFT JOIN {Schema}.[Actions] a ON e.[Id] = a.[PerformerId]
                 LEFT JOIN {Schema}.[Items] ai ON a.[ItemId] = ai.[Id]
             WHERE e.[Id] = @Id
             """;
        
        EmployeeDetails employee = (await connection.QueryAsync
               <EmployeeDetails, RequestSummary?, InventoryItemSummary?, ActionSummary?, InventoryItemSummary?, EmployeeDetails>(
                   sql: query,
                   map: (employee, request, requestItem, action, actionItem) => 
                   { 
                       if (request is not null && requestItem is not null)
                       {
                           request.RequestedItem = requestItem;
                           employee.Requests.Add(request);
                       }
                       if (action is not null && actionItem is not null)
                       {
                           action.InventoryItem = actionItem;
                           employee.Actions.Add(action);
                       }
                       return employee;
                   },
                   splitOn: $"{nameof(RequestSummary.Id)}" + 
                            $", {nameof(InventoryItemSummary.Id)}" + 
                            $", {nameof(ActionSummary.Id)}" + 
                            $", {nameof(InventoryItemSummary.Id)}",
                   param: new { Id = id }))
           .First();

        return employee;
    }

    public async Task<IReadOnlyCollection<EmployeeSummary>> GetAllAsync(
        IPaginationOptions pagination, CancellationToken cancellationToken)
    {
        const string query = 
            $"""
             SELECT
                [Id] AS [{nameof(EmployeeSummary.Id)}],
                [FullName] AS [{nameof(EmployeeSummary.FullName)}],
                [Email] AS [{nameof(EmployeeSummary.Email)}],
                [PhoneNumber] AS [{nameof(EmployeeSummary.PhoneNumber)}],
                [Position] AS [{nameof(EmployeeSummary.Position)}]
             FROM {Schema}.[Employees]
             """;
        
        await using var connection = _connectionFactory.CreateConnection();
        IReadOnlyCollection<EmployeeSummary> employees = (await connection
                .QueryAsync<EmployeeSummary>(query))
            .Filter(pagination.SearchTerm)
            .SortBy(pagination.SortByColumn, pagination.SortOrder)
            .AsList()
            .AsReadOnly();

        return employees;
    }
}