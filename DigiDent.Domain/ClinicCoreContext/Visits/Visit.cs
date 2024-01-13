using DigiDent.Domain.ClinicCoreContext.Doctors.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Patients.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits.Enumerations;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects.Ids;
using DigiDent.Domain.SharedKernel.Abstractions;
using DigiDent.Domain.SharedKernel.ValueObjects;

namespace DigiDent.Domain.ClinicCoreContext.Visits;

public class Visit: AggregateRoot, IEntity<VisitId, Guid>
{
    public VisitId Id { get; init; }
    
    public DoctorId DoctorId { get; private set; }
    public FullName DoctorFullName { get; private set; }
    public Email DoctorEmail { get; private set; }
    public PhoneNumber DoctorPhoneNumber { get; private set; } 
    
    public PatientId PatientId { get; private set; }
    public string PatientFullName { get; private set; }
    public string PatientEmail { get; private set; }
    public PhoneNumber PatientPhoneNumber { get; private set; }
    
    public TreatmentPlanId? TreatmentPlanId { get; private set; }
    public string TreatmentPlanDiagnosis { get; private set; }
    
    public string ProceduresDone { get; private set; }
    
    public DateTime VisitDateTime { get; private set; }
    
    public Money PricePaid { get; private set; }
    public Feedback? Feedback { get; private set; }
    
    public VisitStatus Status { get; private set; }
    
    
    //TODO: here store also additional media files:
    //like computer tomography, x-ray, photos, etc.
    
    
}