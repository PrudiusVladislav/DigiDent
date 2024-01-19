using DigiDent.Domain.ClinicCoreContext.Employees.Doctors;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.Abstractions;
using DigiDent.EFCorePersistence.ClinicCore.Shared.Repositories;

namespace DigiDent.EFCorePersistence.ClinicCore.Employees.Doctors;

public class DoctorRepository: 
    CrudRepository<Doctor, EmployeeId, Guid>,
    IDoctorRepository
{
    public DoctorRepository(ClinicCoreDbContext dbContext) : base(dbContext)
    {
    }
}