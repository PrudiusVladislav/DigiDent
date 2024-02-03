namespace DigiDent.Notification.Application.Abstractions;

public interface IEmailService
{
    Task SendTransactionalEmailAsync(
        string toEmail,
        string subject,
        string htmlPart);
}