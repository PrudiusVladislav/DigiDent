using DigiDent.Domain.ClinicCoreContext.Patients.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits;
using DigiDent.Domain.SharedKernel.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Patients;

public class Patient: AggregateRoot, IEntity<PatientId, Guid>
{
    public PatientId Id { get; init; }
    
    public string FullName { get; private set; }
    public string Email { get; private set; }
    
    public ICollection<Visit> PatientVisits { get; set; } = new List<Visit>();
}