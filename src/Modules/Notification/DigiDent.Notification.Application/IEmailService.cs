namespace DigiDent.Notification.Application;

public interface IEmailService
{
    string CompanyEmail { get; }
    Task SendTransactionalEmailAsync(
        string fromEmail,
        string toEmail,
        string subject,
        string htmlPart);
}