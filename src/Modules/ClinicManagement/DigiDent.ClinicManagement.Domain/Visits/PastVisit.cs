using DigiDent.ClinicManagement.Domain.Employees.Doctors;
using DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects.Ids;
using DigiDent.ClinicManagement.Domain.Patients;
using DigiDent.ClinicManagement.Domain.Patients.ValueObjects;
using DigiDent.ClinicManagement.Domain.Visits.Abstractions;
using DigiDent.ClinicManagement.Domain.Visits.Enumerations;
using DigiDent.ClinicManagement.Domain.Visits.Events;
using DigiDent.ClinicManagement.Domain.Visits.ValueObjects;
using DigiDent.ClinicManagement.Domain.Visits.ValueObjects.Ids;
using DigiDent.Shared.Kernel.Abstractions;

namespace DigiDent.ClinicManagement.Domain.Visits;

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
    
    public VisitDateTime VisitDateTime { get; init; }
    
    public Money PricePaid { get; init; }
    public Feedback? Feedback { get; private set; }
    public VisitStatus Status { get; init; }
    
    /// <summary>
    /// A list of procedures (names) done during the visit.
    /// </summary>
    public IEnumerable<string> ProceduresDone { get; init; }
    
    public PastVisit(
        PastVisitId id,
        EmployeeId doctorId,
        PatientId patientId,
        VisitDateTime visitDateTime,
        Money pricePaid,
        VisitStatus status,
        IEnumerable<string> proceduresDone,
        TreatmentPlanId? treatmentPlanId)
    {
        Id = id;
        DoctorId = doctorId;
        PatientId = patientId;
        VisitDateTime = visitDateTime;
        PricePaid = pricePaid;
        Status = status;
        ProceduresDone = proceduresDone;
        TreatmentPlanId = treatmentPlanId;
        
        PastVisitCreatedDomainEvent pastVisitCreatedDomainEvent = new (
            EventId: Guid.NewGuid(),
            DateTime.Now,
            this);
        
        Raise(pastVisitCreatedDomainEvent);
        
    }
}