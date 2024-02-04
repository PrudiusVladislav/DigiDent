using DigiDent.Notification.Application.Abstractions;
using DigiDent.Notification.Application.ValueObjects;
using DigiDent.UserAccess.IntegrationEvents;
using Rebus.Handlers;

namespace DigiDent.Notification.Application.Users;

public class UserActivatedHandler
    : IHandleMessages<UserActivatedMessage>
{
    private readonly IEmailService _emailService;
    private readonly IEmailContentFactory _emailContentFactory;

    public UserActivatedHandler(
        IEmailService emailService,
        IEmailContentFactory emailContentFactory)
    {
        _emailService = emailService;
        _emailContentFactory = emailContentFactory;
    }

    public async Task Handle(UserActivatedMessage message)
    {
        EmailContent emailContent = _emailContentFactory
            .CreateUserActivatedEmail(message.FullName, message.Email);
        
        await _emailService.SendEmailAsync(
            message.Email, emailContent);
    }
}