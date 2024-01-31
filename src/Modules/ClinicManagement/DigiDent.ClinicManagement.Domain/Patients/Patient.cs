using DigiDent.Domain.ClinicCoreContext.Patients.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits;
using DigiDent.Shared.Domain.Abstractions;
using DigiDent.Shared.Domain.ValueObjects;

namespace DigiDent.Domain.ClinicCoreContext.Patients;

public class Patient: AggregateRoot, IPerson<PatientId>
{
    public PatientId Id { get; init; }
    public FullName FullName { get; private set; }
    public Email Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    
    public Gender Gender { get; set; }
    public DateOnly? DateOfBirth { get; private set; }
    
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public ICollection<PastVisit> PastVisits { get; set; } = new List<PastVisit>();
    public ICollection<TreatmentPlan> TreatmentPlans { get; set; } = new List<TreatmentPlan>();
    
    internal Patient(
        PatientId id,
        FullName fullName,
        Email email,
        PhoneNumber phoneNumber)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
    }
    
    public static Patient Create(PersonCreationArgs args)
    {
        var patientId = TypedId.New<PatientId>();
        return new Patient(
            patientId,
            args.FullName,
            args.Email,
            args.PhoneNumber);
    }
}