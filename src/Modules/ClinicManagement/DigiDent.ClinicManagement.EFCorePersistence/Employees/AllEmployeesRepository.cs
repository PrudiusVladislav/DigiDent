using DigiDent.ClinicManagement.Domain.Employees.Shared;
using DigiDent.ClinicManagement.Domain.Employees.Shared.Abstractions;

namespace DigiDent.ClinicManagement.EFCorePersistence.Employees;

public class AllEmployeesRepository : 
    EmployeesRepository<Employee>,
    IAllEmployeesRepository
{
    public AllEmployeesRepository(ClinicCoreDbContext context)
        : base(context)
    {
    }
}