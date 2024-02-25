﻿using DigiDent.Shared.Kernel.Pagination;

namespace DigiDent.InventoryManagement.Domain.Employees;

public interface IEmployeesQueriesRepository
{
    Task<EmployeeDetails?> GetByIdAsync(
        Guid id, CancellationToken cancellationToken);
    
    Task<IReadOnlyCollection<EmployeeSummary>> GetAllAsync(
        IPaginationOptions pagination, CancellationToken cancellationToken);
}