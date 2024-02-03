using DigiDent.Notification.Application.Abstractions;

namespace DigiDent.Notification.Infrastructure;

public class EmailTemplateFactory: IEmailTemplateFactory
{
    public string CreateInformationEmail(string message)
    {
        return $"<h3>{message}</h3>";
    }

    public string CreateCodeConfirmationEmail(string message, string code)
    {
        return $"<h3>{message}</h3><p>Your code is: {code}</p>";
    }
}