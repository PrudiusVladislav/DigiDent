using DigiDent.Domain.SharedKernel;
using DigiDent.Domain.VisitsScheduleContext.Doctors;
using DigiDent.Domain.VisitsScheduleContext.Doctors.ValueObjects;
using DigiDent.Domain.VisitsScheduleContext.Patients;
using DigiDent.Domain.VisitsScheduleContext.Patients.ValueObjects;
using DigiDent.Domain.VisitsScheduleContext.Visits.Enumerations;
using DigiDent.Domain.VisitsScheduleContext.Visits.ValueObjects;
using DigiDent.Domain.VisitsScheduleContext.Visits.ValueObjects.Ids;

namespace DigiDent.Domain.VisitsScheduleContext.Visits;

public class Visit: IEntity<VisitId, Guid>
{
    public VisitId Id { get; init; }
    
    public DoctorId DoctorId { get; private set; }
    public Doctor Doctor { get; private set; }
    
    public PatientId PatientId { get; private set; }
    public Patient Patient { get; private set; }
    
    public DateTime VisitDateTime { get; private set; }
    
    public ServiceId ServiceId { get; private set; }
    public Service ServiceType { get; private set; }
    
    public Money? PricePaid { get; private set; }
    
    public TreatmentPlanId? TreatmentPlanId { get; private set; }
    public TreatmentPlan? TreatmentPlan { get; private set; }
    
    public VisitStatus Status { get; private set; }
    
    //TODO: here store also additional media files:
    //like computer tomography, x-ray, photos, etc.
}