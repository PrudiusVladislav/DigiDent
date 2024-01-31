using DigiDent.Application.Shared.IntegrationEvents;
using DigiDent.Shared.Domain.ValueObjects;
using DigiDent.Domain.UserAccessContext.Users.Events;
using MediatR;

namespace DigiDent.Application.UserAccess.Commands.SignUp;

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