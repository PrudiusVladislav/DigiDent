﻿namespace DigiDent.InventoryManagement.Domain.Employees;

public interface IEmployeesRepository
{
    Task<Employee?> GetByIdAsync(EmployeeId id, CancellationToken cancellationToken);
    Task AddAsync(Employee employee, CancellationToken cancellationToken);
    Task UpdateAsync(Employee employee, CancellationToken cancellationToken);
}