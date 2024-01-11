using DigiDent.Domain.SharedKernel;
using DigiDent.Domain.VisitsScheduleContext.Patients.ValueObjects;
using DigiDent.Domain.VisitsScheduleContext.Visits;

namespace DigiDent.Domain.VisitsScheduleContext.Patients;

public class Patient: AggregateRoot, IEntity<PatientId, Guid>
{
    public PatientId Id { get; init; }
    
    public string FullName { get; private set; }
    public string Email { get; private set; }
    
    public ICollection<Visit> PatientVisits { get; set; } = new List<Visit>();
}