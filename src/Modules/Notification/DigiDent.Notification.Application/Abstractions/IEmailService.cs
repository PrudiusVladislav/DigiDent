using DigiDent.Notification.Application.ValueObjects;

namespace DigiDent.Notification.Application.Abstractions;

public interface IEmailService
{
    Task SendTransactionalEmailAsync(
        string toEmail, EmailContent emailContent);
}