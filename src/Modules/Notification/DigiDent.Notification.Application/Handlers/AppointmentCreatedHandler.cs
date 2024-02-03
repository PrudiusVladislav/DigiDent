using DigiDent.ClinicManagement.IntegrationEvents;
using DigiDent.Notification.Application.Abstractions;
using Rebus.Handlers;

namespace DigiDent.Notification.Application.Handlers;

public class AppointmentCreatedHandler
    : IHandleMessages<AppointmentCreatedIntegrationEvent>
{
    private readonly IEmailService _emailService;
    private readonly IEmailTemplateFactory _emailTemplateFactory;

    public AppointmentCreatedHandler(
        IEmailService emailService,
        IEmailTemplateFactory emailTemplateFactory)
    {
        _emailService = emailService;
        _emailTemplateFactory = emailTemplateFactory;
    }

    public async Task Handle(AppointmentCreatedIntegrationEvent message)
    {
        string emailHtml = _emailTemplateFactory.CreateInformationEmail(
            $"Dear {message.PatientFullName},<br>" +
            $"We are glad to inform you that your appointment with Dr. {message.DoctorFullName} " +
            $"has been arranged for {message.ArrangedDateTime}.<br>" +
            "Best regards,<br>" +
            "DigiDent team.");
        
        await _emailService.SendTransactionalEmailAsync(
            toEmail: message.PatientEmail,
            subject: "New arranged appointment",
            htmlPart: emailHtml);
    }
}