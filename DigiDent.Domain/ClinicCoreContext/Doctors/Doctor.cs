using DigiDent.Domain.ClinicCoreContext.Doctors.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits;
using DigiDent.Domain.SharedKernel.Abstractions;
using DigiDent.Domain.SharedKernel.ValueObjects;

namespace DigiDent.Domain.ClinicCoreContext.Doctors;

public class Doctor: AggregateRoot, IEntity<DoctorId, Guid>
{
    public DoctorId Id { get; init; }
    
    public FullName FullName { get; private set; }
    public Email Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    
    public string? Specialization { get; private set; }
    public string? Biography { get; private set; }
    
    public ICollection<Service> ProvidedServices { get; set; } = new List<Service>();
    public ICollection<Visit> DoctorVisits { get; set; } = new List<Visit>();
    //public ICollection<Availability> WorkingHours { get; set; } = new List<Availability>();
}