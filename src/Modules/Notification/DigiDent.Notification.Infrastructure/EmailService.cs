using DigiDent.Notification.Application.Abstractions;
using DigiDent.Notification.Application.ValueObjects;
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
    
    public async Task SendEmailAsync(
        string toEmail, EmailContent emailContent)
    {
        var email = new TransactionalEmailBuilder()
            .WithFrom(CompanyEmail)
            .WithSubject(emailContent.Subject)
            .WithHtmlPart(emailContent.HtmlBody)
            .WithTo(new SendContact(toEmail))
            .Build();
        
        await _client.SendTransactionalEmailAsync(email);
    }
}