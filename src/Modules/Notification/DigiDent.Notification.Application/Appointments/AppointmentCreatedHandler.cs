using DigiDent.ClinicManagement.IntegrationEvents;
using DigiDent.Notification.Application.Abstractions;
using DigiDent.Notification.Application.ValueObjects;
using Rebus.Handlers;

namespace DigiDent.Notification.Application.Appointments;

public class AppointmentCreatedHandler
    : IHandleMessages<AppointmentCreatedIntegrationEvent>
{
    private readonly IEmailService _emailService;
    private readonly IEmailContentFactory _emailContentFactory;

    public AppointmentCreatedHandler(
        IEmailService emailService,
        IEmailContentFactory emailContentFactory)
    {
        _emailService = emailService;
        _emailContentFactory = emailContentFactory;
    }

    public async Task Handle(AppointmentCreatedIntegrationEvent message)
    {
        EmailContent appointmentArrangedContent = await _emailContentFactory
            .CreateAppointmentArrangedEmail(
                message.PatientFullName,
                message.DoctorFullName,
                message.ArrangedDateTime);
        
        await _emailService.SendEmailAsync(
            message.PatientEmail, appointmentArrangedContent);
    }
}