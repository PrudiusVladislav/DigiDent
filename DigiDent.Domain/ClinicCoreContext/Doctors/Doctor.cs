using DigiDent.Domain.ClinicCoreContext.Doctors.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits;
using DigiDent.Domain.SharedKernel.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Doctors;

public class Doctor: AggregateRoot, IEntity<DoctorId, Guid>
{
    public DoctorId Id { get; init; }
    
    public string FullName { get; private set; }
    public string Email { get; private set; }
    
    public ICollection<Service> ProvidedServices { get; set; } = new List<Service>();
    public ICollection<Visit> DoctorVisits { get; set; } = new List<Visit>();
}