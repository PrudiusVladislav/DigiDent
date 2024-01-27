using DigiDent.Domain.ClinicCoreContext.Employees.Administrators;

namespace DigiDent.EFCorePersistence.ClinicCore.Employees.Administrators;

public class AdministratorsRepository : EmployeesRepository<Administrator>
{
    public AdministratorsRepository(ClinicCoreDbContext context)
        : base(context)
    {
    }
}