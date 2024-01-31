using DigiDent.ClinicManagement.Domain.Employees.Doctors;
using DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects.Ids;
using DigiDent.ClinicManagement.Domain.Patients;
using DigiDent.ClinicManagement.Domain.Patients.ValueObjects;
using DigiDent.ClinicManagement.Domain.Visits.ValueObjects;
using DigiDent.Shared.Kernel.Abstractions;

namespace DigiDent.ClinicManagement.Domain.Visits.Abstractions;

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