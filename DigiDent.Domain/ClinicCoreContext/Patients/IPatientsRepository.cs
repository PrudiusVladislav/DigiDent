using DigiDent.Domain.ClinicCoreContext.Patients.ValueObjects;

namespace DigiDent.Domain.ClinicCoreContext.Patients;

public interface IPatientsRepository
{
    Task<IReadOnlyCollection<Patient>> GetAllAsync(CancellationToken cancellationToken);
    Task<Patient?> GetByIdAsync(PatientId id, CancellationToken cancellationToken);
    Task UpdateAsync(Patient patient, CancellationToken cancellationToken);
}