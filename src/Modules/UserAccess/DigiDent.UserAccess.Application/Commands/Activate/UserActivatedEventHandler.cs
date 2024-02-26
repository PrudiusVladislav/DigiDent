using DigiDent.UserAccess.Domain.Users.Events;
using DigiDent.UserAccess.IntegrationEvents;
using MediatR;
using Rebus.Bus;

namespace DigiDent.UserAccess.Application.Commands.Activate;

public sealed class UserActivatedEventHandler
    : INotificationHandler<UserActivatedDomainEvent>
{
    private readonly IPublisher _publisher;
    private readonly IBus _bus;

    public UserActivatedEventHandler(IPublisher publisher, IBus bus)
    {
        _publisher = publisher;
        _bus = bus;
    }

    public async Task Handle(
        UserActivatedDomainEvent notification, CancellationToken cancellationToken)
    {
        UserActivatedIntegrationEvent userActivatedIntegrationEvent = new()
        {
            TimeOfOccurrence = notification.TimeOfOccurrence,
            FullName = notification.ActivatedUser.FullName,
            Email = notification.ActivatedUser.Email,
            PhoneNumber = notification.ActivatedUser.PhoneNumber,
            Role = notification.ActivatedUser.Role
        };
        
        await _publisher.Publish(userActivatedIntegrationEvent, cancellationToken);
        
        UserActivatedMessage userActivatedMessage = new()
        {
            FullName = notification.ActivatedUser.FullName.ToString(),
            Email = notification.ActivatedUser.Email.Value
        };
        
        await _bus.Publish(userActivatedMessage);
    }
}