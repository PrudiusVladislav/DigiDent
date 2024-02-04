using DigiDent.ClinicManagement.IntegrationEvents;
using DigiDent.Notification.Application.Abstractions;
using DigiDent.Notification.Application.ValueObjects;
using Rebus.Handlers;

namespace DigiDent.Notification.Application.Appointments;

public class SendPatientReminderHandler
    : IHandleMessages<SendPatientReminderForAppointment>
{
    private readonly IEmailService _emailService;
    private readonly IEmailContentFactory _emailContentFactory;

    public SendPatientReminderHandler(
        IEmailService emailService,
        IEmailContentFactory emailContentFactory)
    {
        _emailService = emailService;
        _emailContentFactory = emailContentFactory;
    }

    public async Task Handle(SendPatientReminderForAppointment message)
    {
        EmailContent reminderContent = _emailContentFactory
            .CreatePatientReminder(
                message.PatientFullName,
                message.DoctorFullName,
                message.ArrangedDateTime);
        
        await _emailService.SendEmailAsync
            (message.PatientEmail, reminderContent);
    }
}