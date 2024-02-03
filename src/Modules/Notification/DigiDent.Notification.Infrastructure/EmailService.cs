using DigiDent.Notification.Application;
using DigiDent.Notification.Application.Abstractions;
using Mailjet.Client;
using Mailjet.Client.TransactionalEmails;

namespace DigiDent.Notification.Infrastructure;

public class EmailService: IEmailService
{
    private static readonly SendContact CompanyEmail = 
        new("prudiusvladyslav@gmail.com");
    
    private readonly IMailjetClient _client;

    public EmailService(IMailjetClient client)
    {
        _client = client;
    }
    
    public async Task SendTransactionalEmailAsync(
        string toEmail, string subject, string htmlPart)
    {
        var email = new TransactionalEmailBuilder()
            .WithFrom(CompanyEmail)
            .WithSubject(subject)
            .WithHtmlPart(htmlPart)
            .WithTo(new SendContact(toEmail))
            .Build();
        
        await _client.SendTransactionalEmailAsync(email);
    }
}