using DigiDent.UserAccess.Domain.Users.Events;
using DigiDent.UserAccess.IntegrationEvents;
using MediatR;

namespace DigiDent.UserAccess.Application.Commands.Activate;

public sealed class UserActivatedEventHandler
    : INotificationHandler<UserActivatedDomainEvent>
{
    private readonly IPublisher _publisher;

    public UserActivatedEventHandler(IPublisher publisher)
    {
        _publisher = publisher;
    }

    public async Task Handle(
        UserActivatedDomainEvent notification, CancellationToken cancellationToken)
    {
        UserActivatedIntegrationEvent userActivatedIntegrationEvent = new()
        {
            EventId = notification.EventId,
            TimeOfOccurrence = notification.TimeOfOccurrence,
            FullName = notification.ActivatedUser.FullName,
            Email = notification.ActivatedUser.Email,
            PhoneNumber = notification.ActivatedUser.PhoneNumber,
            Role = notification.ActivatedUser.Role
        };
        
        await _publisher.Publish(userActivatedIntegrationEvent, cancellationToken);
    }
}