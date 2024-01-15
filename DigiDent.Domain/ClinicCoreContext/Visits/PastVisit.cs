using DigiDent.Domain.ClinicCoreContext.Employees.Doctors;
using DigiDent.Domain.ClinicCoreContext.Employees.Doctors.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Patients;
using DigiDent.Domain.ClinicCoreContext.Patients.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits.Enumerations;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects.Ids;
using DigiDent.Domain.SharedKernel.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Visits;

public class PastVisit : 
    IEntity<VisitId, Guid>
{
    public VisitId Id { get; init; }
    
    public DoctorId DoctorId { get; init; }
    public Doctor Doctor { get; init; } = null!;
    
    public PatientId PatientId { get; init; }
    public Patient Patient { get; init; } = null!;
    
    public TreatmentPlanId? TreatmentPlanId { get; init; }
    public TreatmentPlan? TreatmentPlan { get; init; }
    
    public DateTime VisitDateTime { get; init; }
    
    public Money PricePaid { get; init; }
    public Feedback? Feedback { get; init; }
    public VisitStatus Status { get; init; }
    
    /// <summary>
    /// A list of procedures (names) done during the visit.
    /// </summary>
    //TODO: consider using JSON to store procedures.
    public IEnumerable<string> ProceduresDone { get; init; }
    
    internal PastVisit(
        VisitId id,
        DoctorId doctorId,
        PatientId patientId,
        DateTime visitDateTime,
        Money pricePaid,
        IEnumerable<string> proceduresDone)
    {
        Id = id;
        DoctorId = doctorId;
        PatientId = patientId;
        VisitDateTime = visitDateTime;
        PricePaid = pricePaid;
        ProceduresDone = proceduresDone;
    }
    
    public static PastVisit Create(
        DoctorId doctorId,
        PatientId patientId,
        DateTime visitDateTime,
        Money pricePaid,
        IEnumerable<string> proceduresDone)
    {
        //TODO: if decided to store the procedures as JSON, then
        //consider passing the list of procedures instead of string into the Create method.
        var visitId = TypedId.New<VisitId>();
        return new PastVisit(
            visitId,
            doctorId,
            patientId,
            visitDateTime,
            pricePaid,
            proceduresDone);
    }
}