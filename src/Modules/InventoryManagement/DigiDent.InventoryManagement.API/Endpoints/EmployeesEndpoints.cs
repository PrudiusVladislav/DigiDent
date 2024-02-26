using DigiDent.InventoryManagement.Domain.Employees.Repositories;
using DigiDent.Shared.Infrastructure.Pagination;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace DigiDent.InventoryManagement.API.Endpoints;

public static class EmployeesEndpoints
{
    internal static IEndpointRouteBuilder MapEmployeeEndpoints(
        this IEndpointRouteBuilder endpoints)
    {
        var employeesGroup = endpoints.MapGroup("/employees");

        employeesGroup.MapGet("/", GetAllEmployees);
        employeesGroup.MapGet("/{employeeId}", GetEmployeeById);
        
        return endpoints;
    }

    private static async Task<IResult> GetAllEmployees(
        [AsParameters] PaginationDTO paginationDTO,
        [FromServices] IEmployeesQueriesRepository employeesQueriesRepository,
        CancellationToken cancellationToken)
    {
        var employees = await employeesQueriesRepository.GetAllAsync(
            paginationDTO, cancellationToken);
        
        return Results.Ok(employees);
    }
    
    private static async Task<IResult> GetEmployeeById(
        [FromRoute] Guid employeeId,
        [FromServices] IEmployeesQueriesRepository employeesQueriesRepository,
        CancellationToken cancellationToken)
    {
        var employee = await employeesQueriesRepository.GetByIdAsync(
            employeeId, cancellationToken);
        
        return employee is not null
            ? Results.Ok(employee)
            : Results.NotFound();
    }
}