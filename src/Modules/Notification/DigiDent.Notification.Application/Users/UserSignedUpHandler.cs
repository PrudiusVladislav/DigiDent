using DigiDent.Notification.Application.Abstractions;
using DigiDent.Notification.Application.ValueObjects;
using DigiDent.UserAccess.API;
using DigiDent.UserAccess.IntegrationEvents;
using Rebus.Handlers;

namespace DigiDent.Notification.Application.Users;

public class UserSignedUpHandler: IHandleMessages<UserSignedUpIntegrationEvent>
{
    private readonly IEmailService _emailService;
    private readonly IEmailContentFactory _emailContentFactory;

    public UserSignedUpHandler(
        IEmailService emailService,
        IEmailContentFactory emailContentFactory)
    {
        _emailService = emailService;
        _emailContentFactory = emailContentFactory;
    }

    public async Task Handle(UserSignedUpIntegrationEvent message)
    {
        var activationLink = $"{UserAccessModule.BaseApiUrl}/activate/{message.UserId}";
        
        EmailContent activationEmailContent = _emailContentFactory
            .CreateActivationEmail(message.UserFullName, activationLink);
        
        await _emailService.SendEmailAsync(
            message.UserEmail, activationEmailContent);
    }
}