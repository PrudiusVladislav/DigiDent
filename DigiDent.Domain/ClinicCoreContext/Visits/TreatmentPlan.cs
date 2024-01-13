using DigiDent.Domain.ClinicCoreContext.Patients;
using DigiDent.Domain.ClinicCoreContext.Patients.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits.Enumerations;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects.Ids;
using DigiDent.Domain.SharedKernel.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Visits;

public class TreatmentPlan: IEntity<TreatmentPlanId, Guid>
{
    public TreatmentPlanId Id { get; init; }
    public TreatmentPlanDetails Details { get; private set; }
    
    public PatientId PatientId { get; init; }
    public Patient Patient { get; init; }
    
    public DateOnly DateOfStart { get; init; }
    public DateOnly DateOfFinish { get; private set; }
    
    public TreatmentPlanStatus Status { get; private set; }
    public ICollection<Visit> Visits { get; set; } = new List<Visit>();

    internal TreatmentPlan(
        TreatmentPlanId id,
        TreatmentPlanDetails details,
        PatientId patientId,
        DateOnly dateOfStart,
        TreatmentPlanStatus status)
    {
        Id = id;
        Details = details;
        PatientId = patientId;
        DateOfStart = dateOfStart;
        Status = status;
    }
    
    public static TreatmentPlan Create(
        TreatmentPlanDetails details,
        PatientId patientId)
    {
        return new TreatmentPlan(
            new TreatmentPlanId(Guid.NewGuid()),
            details,
            patientId,
            DateOnly.FromDateTime(DateTime.UtcNow),
            TreatmentPlanStatus.Active);
    }
}