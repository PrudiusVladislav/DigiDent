using DigiDent.Domain.ClinicCoreContext.Employees.Shared;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.Abstractions;

namespace DigiDent.EFCorePersistence.ClinicCore.Employees;

public class AllEmployeesRepository : 
    EmployeesRepository<Employee>,
    IAllEmployeesRepository
{
    public AllEmployeesRepository(ClinicCoreDbContext context)
        : base(context)
    {
    }
}