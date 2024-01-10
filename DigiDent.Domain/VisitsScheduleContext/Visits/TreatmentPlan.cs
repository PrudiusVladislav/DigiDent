using DigiDent.Domain.SharedKernel;
using DigiDent.Domain.VisitsScheduleContext.Patients;
using DigiDent.Domain.VisitsScheduleContext.Patients.ValueObjects;
using DigiDent.Domain.VisitsScheduleContext.Visits.Enumerations;
using DigiDent.Domain.VisitsScheduleContext.Visits.ValueObjects;

namespace DigiDent.Domain.VisitsScheduleContext.Visits;

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