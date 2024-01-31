using DigiDent.Shared.Domain.Abstractions;
using DigiDent.Shared.Domain.ValueObjects;
using DigiDent.UserAccess.Domain.Users.Events;
using DigiDent.UserAccess.Domain.Users.ValueObjects;

namespace DigiDent.UserAccess.Domain.Users;

/// <summary>
/// Represents a User entity - aggregate root within Users aggregate in UserAccess bounded context.
/// </summary>
public class User: AggregateRoot, IEntity<UserId, Guid>
{
    public UserId Id { get; init; } = null!;
    public FullName FullName { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public PhoneNumber PhoneNumber { get; private set; } = null!;
    public Password Password { get; private set; } = null!;
    public Role Role { get; private set; }
    
    // only for EF Core
    private User() { }
    
    public User(
        FullName fullName,
        Email email,
        PhoneNumber phoneNumber,
        Password password,
        Role role)
    {
        Id = TypedId.New<UserId>();
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
        Password = password;
        Role = role;
        
        UserSignedUpDomainEvent userSignedUpEvent = new(
            EventId: Guid.NewGuid(),
            TimeOfOccurrence: DateTime.Now,
            SignedUpUser: this);
        
        Raise(userSignedUpEvent);
    }
}