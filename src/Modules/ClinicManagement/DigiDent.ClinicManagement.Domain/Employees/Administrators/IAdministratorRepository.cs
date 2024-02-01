using DigiDent.ClinicManagement.Domain.Employees.Doctors;
using DigiDent.ClinicManagement.Domain.Employees.Shared.Abstractions;

namespace DigiDent.ClinicManagement.Domain.Employees.Administrators;

public interface IAdministratorRepository: IEmployeesRepository<Doctor>
{
    
}