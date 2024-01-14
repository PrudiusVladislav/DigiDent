using DigiDent.Domain.ClinicCoreContext.Employees.Doctors.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Patients.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits.Enumerations;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects.Ids;
using DigiDent.Domain.SharedKernel.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Visits;

public class Visit: AggregateRoot, IEntity<VisitId, Guid>
{
    public VisitId Id { get; init; }
    
    public DoctorId DoctorId { get; private set; }
    public string DoctorFullName { get; private set; }
    public string DoctorEmail { get; private set; }
    public string DoctorPhoneNumber { get; private set; } 
    
    public PatientId PatientId { get; private set; }
    public string PatientFullName { get; private set; }
    public string PatientEmail { get; private set; }
    public string PatientPhoneNumber { get; private set; }
    
    public TreatmentPlanId? TreatmentPlanId { get; private set; }
    public string TreatmentPlanDiagnosis { get; private set; }
    
    /// <summary>
    /// String with procedures separated by comma.
    /// </summary>
    public string ProceduresDone { get; private set; }
    
    public DateTime VisitDateTime { get; private set; }
    
    public Money PricePaid { get; private set; }
    public Feedback? Feedback { get; private set; }
    
    public VisitStatus Status { get; private set; }
    
    //TODO: here store also additional media files:
    //like computer tomography, x-ray, photos, etc.
    
    internal Visit(
        VisitId id,
        DoctorId doctorId,
        string doctorFullName,
        string doctorEmail,
        string doctorPhoneNumber,
        PatientId patientId,
        string patientFullName,
        string patientEmail,
        string patientPhoneNumber,
        TreatmentPlanId? treatmentPlanId,
        string treatmentPlanDiagnosis,
        string proceduresDone,
        DateTime visitDateTime,
        Money pricePaid,
        Feedback? feedback,
        VisitStatus status)
    {
        Id = id;
        DoctorId = doctorId;
        DoctorFullName = doctorFullName;
        DoctorEmail = doctorEmail;
        DoctorPhoneNumber = doctorPhoneNumber;
        PatientId = patientId;
        PatientFullName = patientFullName;
        PatientEmail = patientEmail;
        PatientPhoneNumber = patientPhoneNumber;
        TreatmentPlanId = treatmentPlanId;
        TreatmentPlanDiagnosis = treatmentPlanDiagnosis;
        ProceduresDone = proceduresDone;
        VisitDateTime = visitDateTime;
        PricePaid = pricePaid;
        Feedback = feedback;
        Status = status;
    }

    public static Visit Create(
        Appointment appointment,
        Money pricePaid,
        Feedback? feedback,
        VisitStatus status)
    {
        var visitId = TypedId.New<VisitId>();
        return new Visit(
            visitId,
            appointment.DoctorId,
            appointment.Doctor.FullName.ToString(),
            appointment.Doctor.Email.ToString(),
            appointment.Doctor.PhoneNumber.ToString(),
            appointment.PatientId,
            appointment.Patient.FullName.ToString(),
            appointment.Patient.Email.ToString(),
            appointment.Patient.PhoneNumber.ToString(),
            appointment.TreatmentPlanId,
            appointment.TreatmentPlan?.Details.Diagnosis?? string.Empty,
            string.Join(", ", appointment.DentalProcedures
                .Select(x => x.Details.Name.ToString())),
            appointment.VisitDateTime,
            pricePaid,
            feedback,
            status);
    }
}