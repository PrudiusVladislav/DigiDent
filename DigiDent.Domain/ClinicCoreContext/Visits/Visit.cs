using DigiDent.Domain.ClinicCoreContext.Employees.Doctors;
using DigiDent.Domain.ClinicCoreContext.Employees.Doctors.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Patients;
using DigiDent.Domain.ClinicCoreContext.Patients.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits.Enumerations;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects.Ids;
using DigiDent.Domain.SharedKernel.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Visits;

public class Visit: AggregateRoot, IEntity<VisitId, Guid>
{
    public VisitId Id { get; init; }
    
    public DoctorId DoctorId { get; init; }
    public Doctor Doctor { get; init; } = null!;
    
    public PatientId PatientId { get; init; }
    public Patient Patient { get; init; } = null!;
    
    public TreatmentPlanId? TreatmentPlanId { get; init; }
    public TreatmentPlan? TreatmentPlan { get; init; }
    
    /// <summary>
    /// String with procedures separated by comma.
    /// </summary>
    //TODO: consider using JSON to store procedures.
    public string ProceduresDone { get; init; }
    
    public DateTime VisitDateTime { get; private set; }
    
    public Money PricePaid { get; private set; }
    public Feedback? Feedback { get; private set; }
    
    public VisitStatus Status { get; private set; }
    
    internal Visit(
        VisitId id,
        DoctorId doctorId,
        PatientId patientId,
        DateTime visitDateTime,
        Money pricePaid,
        string proceduresDone)
    {
        Id = id;
        DoctorId = doctorId;
        PatientId = patientId;
        VisitDateTime = visitDateTime;
        PricePaid = pricePaid;
        ProceduresDone = proceduresDone;
    }
    
    public static Visit Create(
        DoctorId doctorId,
        PatientId patientId,
        DateTime visitDateTime,
        Money pricePaid,
        string proceduresDone)
    {
        //TODO: if decided to store the procedures as JSON, then
        //consider passing the list of procedures instead of string into the Create method.
        var visitId = TypedId.New<VisitId>();
        return new Visit(
            visitId,
            doctorId,
            patientId,
            visitDateTime,
            pricePaid,
            proceduresDone);
    }
}