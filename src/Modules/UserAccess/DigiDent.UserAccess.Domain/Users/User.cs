using DigiDent.Shared.Kernel.Abstractions;
using DigiDent.Shared.Kernel.ReturnTypes;
using DigiDent.Shared.Kernel.ValueObjects;
using DigiDent.UserAccess.Domain.Users.Errors;
using DigiDent.UserAccess.Domain.Users.Events;
using DigiDent.UserAccess.Domain.Users.ValueObjects;
using Email = DigiDent.Shared.Kernel.ValueObjects.Email;
using PhoneNumber = DigiDent.Shared.Kernel.ValueObjects.PhoneNumber;

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
    public Status Status { get; internal set; }
    
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
        Status = Status.SignedUp;
        
        UserSignedUpDomainEvent userSignedUpEvent = new(
            TimeOfOccurrence: DateTime.Now,
            SignedUpUser: this);
        
        Raise(userSignedUpEvent);
    }
    
    public Result Activate()
    {
        if (Status == Status.Activated)
        {
            return Result.Fail(UserErrors.UserAlreadyActivated(Id));
        }
        
        Status = Status.Activated;
        
        UserActivatedDomainEvent userActivatedEvent = new(
            TimeOfOccurrence: DateTime.Now,
            ActivatedUser: this);
        
        Raise(userActivatedEvent);
        
        return Result.Ok();
    }
 }