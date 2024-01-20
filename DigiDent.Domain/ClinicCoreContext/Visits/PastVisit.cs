using DigiDent.Domain.ClinicCoreContext.Employees.Doctors;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects.Ids;
using DigiDent.Domain.ClinicCoreContext.Patients;
using DigiDent.Domain.ClinicCoreContext.Patients.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Visits.Enumerations;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects.Ids;
using DigiDent.Domain.SharedKernel.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Visits;

public class PastVisit : 
    AggregateRoot,
    IVisit<PastVisitId, Guid>
{
    public PastVisitId Id { get; init; }
    
    public EmployeeId DoctorId { get; init; }
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
    public IEnumerable<string> ProceduresDone { get; init; }
    
    internal PastVisit(
        PastVisitId id,
        EmployeeId doctorId,
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
        EmployeeId doctorId,
        PatientId patientId,
        DateTime visitDateTime,
        Money pricePaid,
        IEnumerable<string> proceduresDone)
    {
        var visitId = TypedId.New<PastVisitId>();
        return new PastVisit(
            visitId,
            doctorId,
            patientId,
            visitDateTime,
            pricePaid,
            proceduresDone);
    }
}