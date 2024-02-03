using DigiDent.UserAccess.Domain.Users.Events;
using DigiDent.UserAccess.IntegrationEvents;
using MediatR;

namespace DigiDent.UserAccess.Application.Commands.SignUp;

public sealed class UserSignedUpEventHandler 
    : INotificationHandler<UserSignedUpDomainEvent>
{
    private readonly IPublisher _publisher;

    public UserSignedUpEventHandler(IPublisher publisher)
    {
        _publisher = publisher;
    }

    public async Task Handle(
        UserSignedUpDomainEvent notification,
        CancellationToken cancellationToken)
    {
        UserSignedUpIntegrationEvent userSignedUpEvent = new()
        {
            EventId =notification.EventId,
            TimeOfOccurrence = notification.TimeOfOccurrence,
            FullName = notification.SignedUpUser.FullName,
            Email = notification.SignedUpUser.Email,
            PhoneNumber = notification.SignedUpUser.PhoneNumber,
            Role = notification.SignedUpUser.Role
        };
        
        await _publisher.Publish(userSignedUpEvent, cancellationToken);
    }
}