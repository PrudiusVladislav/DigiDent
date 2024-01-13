using DigiDent.Domain.ClinicCoreContext.Doctors.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Shared.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits;
using DigiDent.Domain.SharedKernel.Abstractions;
using DigiDent.Domain.SharedKernel.ValueObjects;

namespace DigiDent.Domain.ClinicCoreContext.Doctors;

public class Doctor:
    AggregateRoot,
    IEntity<DoctorId, Guid>,
    IPerson,
    IEmployee
{
    public DoctorId Id { get; init; }
    
    public FullName FullName { get; private set; }
    public Email Email { get; private set; }
    
    public PhoneNumber PhoneNumber { get; private set; }
    public Gender Gender { get; }
    public DateTime DateOfBirth { get; private set; }
    
    public DoctorSpecialization Specialization { get; private set; }
    public string? Biography { get; private set; }
    
    public ICollection<DentalProcedure> ProvidedServices { get; set; } = new List<DentalProcedure>();
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public ICollection<Visit> PastVisits { get; set; } = new List<Visit>();
    public ICollection<WorkingDay> WorkingDays { get; set; } = new List<WorkingDay>();
}