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
        UserSignedUpIntegrationEvent userSignedUpEvent = new(
            notification.EventId,
            notification.TimeOfOccurrence,
            notification.SignedUpUser.FullName,
            notification.SignedUpUser.Email,
            notification.SignedUpUser.PhoneNumber,
            notification.SignedUpUser.Role);
        
        await _publisher.Publish(userSignedUpEvent, cancellationToken);
    }
}