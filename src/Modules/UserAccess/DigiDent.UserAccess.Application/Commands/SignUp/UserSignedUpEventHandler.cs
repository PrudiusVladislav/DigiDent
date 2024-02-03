using DigiDent.UserAccess.Domain.Users.Events;
using DigiDent.UserAccess.IntegrationEvents;
using MediatR;
using Rebus.Bus;

namespace DigiDent.UserAccess.Application.Commands.SignUp;

public sealed class UserSignedUpEventHandler 
    : INotificationHandler<UserSignedUpDomainEvent>
{
    private readonly IBus _bus;

    public UserSignedUpEventHandler(IBus bus)
    {
        _bus = bus;
    }
    
    public async Task Handle(
        UserSignedUpDomainEvent notification,
        CancellationToken cancellationToken)
    {
        UserSignedUpIntegrationEvent userSignedUpEvent = new()
        {
            EventId =notification.EventId,
            TimeOfOccurrence = notification.TimeOfOccurrence,
            UserFullName = notification.SignedUpUser.FullName.ToString(),
            UserEmail = notification.SignedUpUser.Email.Value,
            UserId = notification.SignedUpUser.Id.Value
        };
        
        await _bus.Publish(userSignedUpEvent);
    }
}