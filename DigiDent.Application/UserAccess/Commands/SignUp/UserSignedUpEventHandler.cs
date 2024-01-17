using DigiDent.Domain.UserAccessContext.Users.Events;
using MediatR;

namespace DigiDent.Application.UserAccess.Commands.SignUp;

public class UserSignedUpEventHandler: INotificationHandler<UserSignedUpDomainEvent>
{
    public Task Handle(UserSignedUpDomainEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}