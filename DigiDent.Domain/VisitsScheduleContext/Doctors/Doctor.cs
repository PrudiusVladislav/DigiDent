using DigiDent.Domain.SharedKernel;
using DigiDent.Domain.VisitsScheduleContext.Doctors.ValueObjects;
using DigiDent.Domain.VisitsScheduleContext.Visits;

namespace DigiDent.Domain.VisitsScheduleContext.Doctors;

public class Doctor: AggregateRoot, IEntity<DoctorId, Guid>
{
    public DoctorId Id { get; init; }
    
    public string FullName { get; private set; }
    public string Email { get; private set; }
    
    public ICollection<Service> ProvidedServices { get; set; } = new List<Service>();
    public ICollection<Visit> DoctorVisits { get; set; } = new List<Visit>();
}