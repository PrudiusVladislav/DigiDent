using DigiDent.Notification.Application;
using Mailjet.Client;
using Mailjet.Client.TransactionalEmails;

namespace DigiDent.Notification.Infrastructure;

public class EmailService: IEmailService
{
    private readonly IMailjetClient _client;

    public EmailService(IMailjetClient client)
    {
        _client = client;
    }

    public string CompanyEmail => "prudiusvladyslav@gmail.com";
    
    public async Task SendTransactionalEmailAsync(
        string fromEmail, string toEmail, string subject, string htmlPart)
    {
        var email = new TransactionalEmailBuilder()
            .WithFrom(new SendContact(fromEmail))
            .WithSubject(subject)
            .WithHtmlPart(htmlPart)
            .WithTo(new SendContact("vladvlpr006@gmail.com"))
            .Build();
        
        await _client.SendTransactionalEmailAsync(email);
    }
}