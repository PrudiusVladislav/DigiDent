using Dapper;
using DigiDent.InventoryManagement.Domain.Actions.ReadModels;
using DigiDent.InventoryManagement.Domain.Employees;
using DigiDent.InventoryManagement.Domain.Items.ReadModels;
using DigiDent.InventoryManagement.Domain.Items.ValueObjects;
using DigiDent.InventoryManagement.Domain.Requests.ReadModels;
using DigiDent.InventoryManagement.Persistence.Constants;
using DigiDent.InventoryManagement.Persistence.Shared;
using DigiDent.Shared.Infrastructure.Persistence.Factories;
using DigiDent.Shared.Kernel.Pagination;
using DigiDent.Shared.Kernel.ValueObjects;

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
        if (!await ExistsAsync(id))
        {
            return null;
        }
        
        const string query = $@"
            SELECT 
                e.[Id] as EmployeeId,
                e.[FullName] as EmployeeFullName,
                e.[Email] as EmployeeEmail,
                e.[PhoneNumber] as EmployeePhoneNumber,
                e.[Role] as EmployeeRole
                r.[Id] as RequestId,
                r.[Status] as RequestStatus,
                r.[RequestedQuantity] as RequestedQuantity,
                r.[DateOfRequest] as DateOfRequest,
                r.[Remarks] as RequestRemarks,
                ri.[Id] as RequestItemId,
                ri.[Name] as RequestItemName,
                ri.[Quantity] as RequestItemQuantity,
                ri.[Category] as RequestItemCategory,
                a.[Id] as ActionId,
                a.[Type] as ActionType,
                a.[Quantity] as ActionQuantity,
                a.[Date] as ActionDate,
                ai.[Id] as ActionItemId,
                ai.[Name] as ActionItemName,
                ai.[Quantity] as ActionItemQuantity,
                ai.[Category] as ActionItemCategory
            FROM {Schema}.[Employees] e
                LEFT JOIN {Schema}.[Requests] r ON e.[Id] = r.[RequesterId]
                LEFT JOIN {Schema}.[Items] ri ON r.[ItemId] = ri.[Id]
                LEFT JOIN {Schema}.[Actions] a ON e.[Id] = a.[PerformerId]
                LEFT JOIN {Schema}.[Items] ai ON a.[ItemId] = ai.[Id]
            WHERE e.[Id] = @Id";
       
        await using var connection = _connectionFactory.CreateConnection();
        
        EmployeeDetails employee = (await connection.QueryAsync
               <EmployeeDetails, RequestSummary?, InventoryItemSummary, ActionSummary?, InventoryItemSummary, EmployeeDetails>(
                   sql: query,
                   map: (employee, request, requestItem, action, actionItem) =>
                   { 
                       if (request is not null)
                       { 
                           request.RequestedItem = requestItem with 
                           {
                               Category = requestItem.Category.ToEnumName<ItemCategory>()
                           };
                           employee.Requests.Add(request);
                       }
                       
                       if (action is not null)
                       { 
                           action.InventoryItem = actionItem with 
                           { 
                               Category = requestItem.Category.ToEnumName<ItemCategory>()
                           }; 
                           employee.Actions.Add(action);
                       }

                       return employee with
                       {
                           Role = employee.Role.ToEnumName<Role>()
                       };
                   },
                   splitOn: "RequestId, RequestItemId, ActionId, ActionItemId",
                   param: new { Id = id }))
           .First();

        return employee;
    }

    private async Task<bool> ExistsAsync(Guid id)
    {
        const string query = $@"
            SELECT 
                COUNT(*)
            FROM {Schema}.[Employees] e
            WHERE e.[Id] = @Id";
        
        await using var connection = _connectionFactory.CreateConnection();
        int count = await connection.ExecuteScalarAsync<int>(
            sql: query,
            param: new { Id = id });
        return count > 0;
    }

    public async Task<IReadOnlyCollection<EmployeeSummary>> GetAllAsync(
        IPaginationOptions pagination, CancellationToken cancellationToken)
    {
        const string query = $@"
            SELECT 
                e.[Id],
                e.[FullName],
                e.[Email],
                e.[PhoneNumber],
                e.[Role]
            FROM {Schema}.[Employees] e";
        
        await using var connection = _connectionFactory.CreateConnection();
        IReadOnlyCollection<EmployeeSummary> employees = (await connection.QueryAsync<EmployeeSummary>(
            sql: query))
            .Select(employee => employee with
            {
                Role = Enum.Parse<Role>(employee.Role).ToString()
            })
            .Filter(pagination.SearchTerm)
            .SortBy(pagination.SortByColumn, pagination.SortOrder)
            .AsList()
            .AsReadOnly();

        return employees;
    }
}