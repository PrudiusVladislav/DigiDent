using DigiDent.Domain.ClinicCoreContext.Employees.Doctors;
using DigiDent.Domain.ClinicCoreContext.Employees.Shared.ValueObjects.Ids;
using DigiDent.Domain.ClinicCoreContext.Patients;
using DigiDent.Domain.ClinicCoreContext.Patients.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Shared.ValueObjects;
using DigiDent.Domain.ClinicCoreContext.Visits.Abstractions;
using DigiDent.Domain.ClinicCoreContext.Visits.Enumerations;
using DigiDent.Domain.ClinicCoreContext.Visits.ValueObjects.Ids;
using DigiDent.Domain.SharedKernel.Abstractions;

namespace DigiDent.Domain.ClinicCoreContext.Visits;

public class Appointment : 
    AggregateRoot,
    IVisit<AppointmentId, Guid>
{
    public AppointmentId Id { get; init; }
    
    public EmployeeId DoctorId { get; private set; }
    public Doctor Doctor { get; private set; } = null!;
    
    public PatientId PatientId { get; private set; }
    public Patient Patient { get; private set; } = null!;
    
    public DateTime VisitDateTime { get; private set; }
    public TimeDuration Duration { get; private set; }
    
    public TreatmentPlanId? TreatmentPlanId { get; private set; }
    public TreatmentPlan? TreatmentPlan { get; private set; }
    
    public AppointmentStatus Status { get; private set; }
    
    public ICollection<ProvidedService> ProvidedServices { get; set; } 
        = new List<ProvidedService>();

    // for EF core
    internal Appointment()
    {
    }

    internal Appointment(
        AppointmentId id,
        EmployeeId doctorId,
        PatientId patientId,
        DateTime visitDateTime,
        TimeDuration duration,
        AppointmentStatus status,
        IEnumerable<ProvidedService> providedServices)
    {
        Id = id;
        DoctorId = doctorId;
        PatientId = patientId;
        VisitDateTime = visitDateTime;
        Duration = duration;
        Status = status;
        
        foreach (var service in providedServices)
        {
            ProvidedServices.Add(service);
        }
    }
    
    public static Appointment Create(
        EmployeeId doctorId,
        PatientId patientId,
        DateTime visitDateTime,
        TimeDuration duration,
        IEnumerable<ProvidedService> providedServices)
    {
            
        var appointmentId = TypedId.New<AppointmentId>();
        return new Appointment(
            appointmentId,
            doctorId,
            patientId,
            visitDateTime,
            duration,
            AppointmentStatus.Scheduled,
            providedServices);
    }
}