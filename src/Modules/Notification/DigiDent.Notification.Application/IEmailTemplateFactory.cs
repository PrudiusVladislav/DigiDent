namespace DigiDent.Notification.Application;

public interface IEmailTemplateFactory
{
    string CreateInformationEmail(string message);
    string CreateCodeConfirmationEmail(string message, string code);
}