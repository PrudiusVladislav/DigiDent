using DigiDent.Domain.ClinicCoreContext.Patients.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Shared.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Patients;

public interface IPatientRepository: ICrudRepository<Patient, PatientId, Guid>
{
    
}