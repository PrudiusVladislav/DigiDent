using DigiDent.Domain.SharedKernel.Abstractions;
using DigiDent.Domain.SharedKernel.ValueObjects;
using DigiDent.Domain.UserAccessContext.Users.Events;
using DigiDent.Domain.UserAccessContext.Users.ValueObjects;

namespace DigiDent.Domain.UserAccessContext.Users;

/// <summary>
/// Represents a User entity - aggregate root within Users aggregate in UserAccess bounded context.
/// </summary>
public class User: AggregateRoot, IEntity<UserId, Guid>
{
    public UserId Id { get; init; }
    public FullName FullName { get; private set; }
    public Email Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public Password Password { get; private set; }
    public Role Role { get; private set; }
    
    // only for EF Core
    private User() { }
    
    internal User(
        UserId id,
        FullName fullName,
        Email email,
        PhoneNumber phoneNumber,
        Password password,
        Role role)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
        Password = password;
        Role = role;
    }
    
    public static User Create(
        FullName fullName,
        Email email,
        PhoneNumber phoneNumber,
        Password password,
        Role role)
    {
        UserId id = new(Guid.NewGuid());
        User user = new(id, fullName, email, phoneNumber, password, role);
        
        UserSignedUpDomainEvent userSignedUpEvent = new(
            EventId: Guid.NewGuid(),
            TimeOfOccurrence: DateTime.UtcNow,
            SignedUpUser: user);
        
        user.Raise(userSignedUpEvent);
        
        return user;
    }
    
    internal static User TempAdmin 
        => new (
            new UserId(Guid.NewGuid()), 
            new FullName("Temporary", "Administrator"),
            Email.TempAdminEmail, 
            PhoneNumber.TempAdminPhoneNumber, 
            Password.TempAdminPassword, 
            Role.Administrator);
}