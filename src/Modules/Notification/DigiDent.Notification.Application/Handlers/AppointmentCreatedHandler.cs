using DigiDent.ClinicManagement.IntegrationEvents;
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
            $"Dear {message.PatientFullName.ToString()},<br>" +
            $"We are glad to inform you that your appointment with Dr. {message.DoctorFullName.ToString()} " +
            $"has been arranged for {message.ArrangedDateTime}.<br>" +
            "Best regards,<br>" +
            "DigiDent team.");
        
        await _emailService.SendTransactionalEmailAsync(
            fromEmail: _emailService.CompanyEmail,
            toEmail: message.PatientEmail.Value,
            subject: "New arranged appointment",
            htmlPart: emailHtml);
    }
}