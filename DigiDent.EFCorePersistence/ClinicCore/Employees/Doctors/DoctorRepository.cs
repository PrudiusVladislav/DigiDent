using DigiDent.Domain.ClinicCoreContext.Employees.Doctors;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.Abstractions;
using DigiDent.EFCorePersistence.ClinicCore.Shared.Repositories;

namespace DigiDent.EFCorePersistence.ClinicCore.Employees.Doctors;

public class DoctorRepository: IDoctorRepository
{
    public DoctorRepository(ClinicCoreDbContext dbContext)
    {
    }
    
    public Task<Doctor?> GetByIdAsync(EmployeeId id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<Doctor>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Doctor entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Doctor entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(EmployeeId id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}