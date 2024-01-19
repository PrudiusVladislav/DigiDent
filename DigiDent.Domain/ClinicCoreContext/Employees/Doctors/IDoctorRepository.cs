using DigiDent.Domain.ClinicCoreContext.Employees.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Shared.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Employees.Doctors;

public interface IDoctorRepository: ICrudRepository<Doctor, EmployeeId, Guid>
{
    
}