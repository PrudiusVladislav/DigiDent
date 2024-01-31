using DigiDent.ClinicManagement.Domain.Patients.ValueObjects;

namespace DigiDent.ClinicManagement.Domain.Patients;

public interface IPatientsRepository
{
    Task<IReadOnlyCollection<Patient>> GetAllAsync(CancellationToken cancellationToken);
    Task<Patient?> GetByIdAsync(PatientId id, CancellationToken cancellationToken);
    Task UpdateAsync(Patient patient, CancellationToken cancellationToken);
}