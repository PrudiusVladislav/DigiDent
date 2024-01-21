using DigiDent.Domain.ClinicCoreContext.Employees.Doctors;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Employees.Administrators;

public interface IAdministratorRepository: IEmployeesRepository<Doctor>
{
    
}