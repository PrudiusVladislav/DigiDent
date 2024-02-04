using DigiDent.Notification.Application.ValueObjects;

namespace DigiDent.Notification.Application.Abstractions;

public interface IEmailService
{
    Task SendEmailAsync(
        string toEmail, EmailContent emailContent);
}