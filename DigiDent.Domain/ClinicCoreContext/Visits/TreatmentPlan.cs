using DigiDent.Domain.ClinicCoreContext.Patients;
using DigiDent.Domain.ClinicCoreContext.Patients.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits.Enumerations;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects.Ids;
using DigiDent.Domain.SharedKernel.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Visits;

public class TreatmentPlan: IEntity<TreatmentPlanId, Guid>
{
    public TreatmentPlanId Id { get; init; }
    
    public string? Diagnosis { get; init; }
    public string? PlanDescription { get; private set; }
    
    public PatientId PatientId { get; init; }
    public Patient Patient { get; init; }
    
    public DateTime TimeOfStart { get; init; }
    public DateTime TimeOfFinish { get; private set; }
    
    public TreatmentPlanStatus Status { get; private set; }
    public ICollection<Visit> Visits { get; set; } = new List<Visit>();
}