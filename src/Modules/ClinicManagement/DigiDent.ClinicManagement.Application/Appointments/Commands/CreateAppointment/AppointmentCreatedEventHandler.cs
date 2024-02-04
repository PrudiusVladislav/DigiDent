using DigiDent.ClinicManagement.Application.Appointments.Commands.Constants;
using DigiDent.ClinicManagement.Domain.Visits.Events;
using DigiDent.ClinicManagement.IntegrationEvents;
using MediatR;
using Rebus.Bus;

namespace DigiDent.ClinicManagement.Application.Appointments.Commands.CreateAppointment;

public class AppointmentCreatedEventHandler
    : INotificationHandler<AppointmentCreatedDomainEvent>
{
    private readonly IBus _bus;

    public AppointmentCreatedEventHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(
        AppointmentCreatedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        string patientEmail = notification.Appointment.Patient.Email.Value;
        string patientFullName = notification.Appointment.Patient.FullName.ToString();
        DateTime arrangedDateTime = notification.Appointment.VisitDateTime.Value;
        string doctorFullName = notification.Appointment.Doctor.FullName.ToString();
        
        AppointmentCreatedIntegrationEvent integrationEvent = new()
        {
            EventId = notification.EventId,
            TimeOfOccurrence = notification.TimeOfOccurrence,
            PatientEmail = patientEmail,
            PatientFullName = patientFullName,
            ArrangedDateTime = arrangedDateTime,
            DoctorFullName = doctorFullName
        };
        
        await _bus.Publish(integrationEvent);

        await ScheduleSendingPatientReminderForAppointment(
            patientEmail, patientFullName, arrangedDateTime, doctorFullName);
    }
    
    private async Task ScheduleSendingPatientReminderForAppointment(
        string patientEmail,
        string patientFullName,
        DateTime arrangedDateTime,
        string doctorFullName)
    {
        //reminder for patient
        DateTime deferUntil = arrangedDateTime.Subtract(
            AppointmentConstants.PatientAppointmentReminderTime);
        
        TimeSpan reminderDelay = deferUntil.Subtract(DateTime.UtcNow);
        
        SendPatientReminderForAppointment reminder = new()
        {
            PatientEmail = patientEmail,
            PatientFullName = patientFullName,
            ArrangedDateTime = arrangedDateTime,
            DoctorFullName = doctorFullName
        };

        await _bus.Defer(reminderDelay, reminder);
    }
}