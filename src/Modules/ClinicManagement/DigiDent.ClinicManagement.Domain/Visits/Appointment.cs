using DigiDent.ClinicManagement.Domain.Employees.Doctors;
using DigiDent.ClinicManagement.Domain.Employees.Shared.ValueObjects.Ids;
using DigiDent.ClinicManagement.Domain.Patients;
using DigiDent.ClinicManagement.Domain.Patients.ValueObjects;
using DigiDent.ClinicManagement.Domain.Shared.ValueObjects;
using DigiDent.ClinicManagement.Domain.Visits.Abstractions;
using DigiDent.ClinicManagement.Domain.Visits.Enumerations;
using DigiDent.ClinicManagement.Domain.Visits.Errors;
using DigiDent.ClinicManagement.Domain.Visits.Events;
using DigiDent.ClinicManagement.Domain.Visits.ValueObjects;
using DigiDent.ClinicManagement.Domain.Visits.ValueObjects.Ids;
using DigiDent.Shared.Kernel.Abstractions;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.ClinicManagement.Domain.Visits;

public class Appointment : 
    AggregateRoot,
    IVisit<AppointmentId, Guid>
{
    public AppointmentId Id { get; init; }
    
    public EmployeeId DoctorId { get; private set; }
    public Doctor Doctor { get; private set; } = null!;
    
    public PatientId PatientId { get; private set; }
    public Patient Patient { get; private set; } = null!;
    
    public VisitDateTime VisitDateTime { get; private set; }
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
        VisitDateTime visitDateTime,
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
        VisitDateTime visitDateTime,
        TimeDuration duration,
        IEnumerable<ProvidedService> providedServices)
    {
        var appointmentId = TypedId.New<AppointmentId>();
        Appointment appointment = new(
            appointmentId,
            doctorId,
            patientId,
            visitDateTime,
            duration,
            AppointmentStatus.Scheduled,
            providedServices);

        AppointmentCreatedDomainEvent appointmentCreatedEvent = new(
            DateTime.Now,
            appointment);
        
        appointment.Raise(appointmentCreatedEvent);
        
        return appointment;
    }

    public Result Close(
        VisitStatus closureStatus,
        Money pricePaid,
        IDateTimeProvider dateTimeProvider)
    {
        if (closureStatus == VisitStatus.Completed && pricePaid == Money.Zero)
            return Result.Fail(AppointmentErrors
                .PricePaidIsZeroWhenStatusIsComplete);
        
        if (closureStatus != VisitStatus.Completed && pricePaid != Money.Zero)
            return Result.Fail(AppointmentErrors
                .PricePaidIsNotZeroWhenStatusIsNotComplete);

        if (closureStatus != VisitStatus.Canceled &&
            dateTimeProvider.Now < VisitDateTime.Value)
        {
            return Result.Fail(AppointmentErrors
                .ClosureStatusIsNotCanceledWhenClosingBeforeVisit);
        }

        AppointmentClosedDomainEvent appointmentClosedEvent = new(
            TimeOfOccurrence: DateTime.Now,
            closureStatus,
            pricePaid,
            ClosedAppointment: this);
        
        Raise(appointmentClosedEvent);
        
        return Result.Ok();
    }
}