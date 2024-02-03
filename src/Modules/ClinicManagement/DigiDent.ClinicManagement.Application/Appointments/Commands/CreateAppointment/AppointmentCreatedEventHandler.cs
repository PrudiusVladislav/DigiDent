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
        AppointmentCreatedIntegrationEvent integrationEvent = new()
        {
            EventId = notification.EventId,
            TimeOfOccurrence = notification.TimeOfOccurrence,
            PatientEmail = notification.Appointment.Patient.Email.Value,
            PatientFullName = notification.Appointment.Patient.FullName.ToString(),
            ArrangedDateTime = notification.Appointment.VisitDateTime.Value,
            DoctorFullName = notification.Appointment.Doctor.FullName.ToString()
        };
        
       await _bus.Publish(integrationEvent);
    }
}