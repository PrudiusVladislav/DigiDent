using DigiDent.Domain.ClinicCoreContext.Patients.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits;
using DigiDent.Domain.SharedKernel.Abstractions;
using DigiDent.Domain.SharedKernel.ValueObjects;

namespace DigiDent.Domain.ClinicCoreContext.Patients;

public class Patient:
    AggregateRoot,
    IEntity<PatientId, Guid>,
    IPerson
{
    public PatientId Id { get; init; }
    
    public FullName FullName { get; private set; }
    public Email Email { get; private set; }
    
    public PhoneNumber PhoneNumber { get; private set; }
    public Gender Gender { get; }
    public DateTime? DateOfBirth { get; private set; }
    
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public ICollection<Visit> Visits { get; set; } = new List<Visit>();
    public ICollection<TreatmentPlan> TreatmentPlans { get; set; } = new List<TreatmentPlan>();
}