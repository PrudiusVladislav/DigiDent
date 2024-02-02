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
        AppointmentCreatedIntegrationEvent integrationEvent = new(
            notification.EventId,
            notification.TimeOfOccurrence,
            PatientEmail: notification.Appointment.Patient.Email,
            PatientFullName: notification.Appointment.Patient.FullName,
            ArrangedDateTime: notification.Appointment.VisitDateTime.Value,
            DoctorFullName: notification.Appointment.Doctor.FullName);
        
       await _bus.Publish(integrationEvent);
    }
}