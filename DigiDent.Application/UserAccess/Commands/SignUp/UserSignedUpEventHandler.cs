using DigiDent.Application.Shared.IntegrationEvents;
using DigiDent.Domain.SharedKernel.ValueObjects;
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
        var userSignedUpIntegrationEvent = new UserSignedUpIntegrationEvent(
            notification.Id,
            notification.TimeOfOccurrence,
            notification.User.FullName,
            notification.User.Email,
            notification.User.PhoneNumber,
            notification.User.Role);
        
        await _publisher.Publish(userSignedUpIntegrationEvent, cancellationToken);
    }
}