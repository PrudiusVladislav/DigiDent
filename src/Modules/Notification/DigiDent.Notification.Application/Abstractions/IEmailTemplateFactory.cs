namespace DigiDent.Notification.Application.Abstractions;

public interface IEmailTemplateFactory
{
    string CreateInformationEmail(string message);
    string CreateCodeConfirmationEmail(string message, string code);
}