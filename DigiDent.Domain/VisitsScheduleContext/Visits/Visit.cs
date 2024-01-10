using DigiDent.Domain.SharedKernel;
using DigiDent.Domain.VisitsScheduleContext.Doctors;
using DigiDent.Domain.VisitsScheduleContext.Doctors.ValueObjects;
using DigiDent.Domain.VisitsScheduleContext.Patients;
using DigiDent.Domain.VisitsScheduleContext.Patients.ValueObjects;
using DigiDent.Domain.VisitsScheduleContext.Visits.Enumerations;
using DigiDent.Domain.VisitsScheduleContext.Visits.ValueObjects;

namespace DigiDent.Domain.VisitsScheduleContext.Visits;

public class Visit: AggregateRoot, IEntity<VisitId, Guid>
{
    public VisitId Id { get; init; }
    
    public DoctorId DoctorId { get; private set; }
    public Doctor Doctor { get; private set; }
    
    public PatientId PatientId { get; private set; }
    public Patient Patient { get; private set; }
    
    public DateTime VisitDate { get; private set; }
    
    public TreatmentPlanId? TreatmentPlanId { get; private set; }
    public TreatmentPlan? TreatmentPlan { get; private set; }
    
    public VisitStatus Status { get; private set; }
}