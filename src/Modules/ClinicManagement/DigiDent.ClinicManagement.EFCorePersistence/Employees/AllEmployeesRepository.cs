using DigiDent.ClinicManagement.Domain.Employees.Shared;
using DigiDent.ClinicManagement.Domain.Employees.Shared.Abstractions;

namespace DigiDent.ClinicManagement.EFCorePersistence.Employees;

public class AllEmployeesRepository : 
    EmployeesRepository<Employee>,
    IAllEmployeesRepository
{
    public AllEmployeesRepository(ClinicManagementDbContext context)
        : base(context)
    {
    }
}