using DigiDent.Domain.ClinicCoreContext.Employees.Doctors;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects.Ids;
using DigiDent.Domain.ClinicCoreContext.Patients;
using DigiDent.Domain.ClinicCoreContext.Patients.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects;
using DigiDent.Shared.Domain.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Visits.Abstractions;

public interface IVisit<TId, TIdValue> 
    : IEntity<TId, TIdValue>, IAggregateRoot
    where TId: IVisitId<TIdValue>
    where TIdValue: notnull
{
    EmployeeId DoctorId { get; }
    Doctor Doctor { get; }
    
    PatientId PatientId { get; }
    Patient Patient { get; }
    
    VisitDateTime VisitDateTime { get; }
    
    TreatmentPlanId? TreatmentPlanId { get; }
    TreatmentPlan? TreatmentPlan { get; }
}