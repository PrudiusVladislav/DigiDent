﻿using DigiDent.Domain.SharedKernel.Abstractions;
using DigiDent.Domain.SharedKernel.ValueObjects;
using DigiDent.Domain.UserAccessContext.Users.DTO;
using DigiDent.Domain.UserAccessContext.Users.ValueObjects;

namespace DigiDent.Domain.UserAccessContext.Users;

public class User: AggregateRoot, IEntity<UserId, Guid>
{
    public UserId Id { get; init; }
    public FullName FullName { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public Role Role { get; private set; }
    
    // only for EF Core
    private User() { }
    
    internal User(UserId id, FullName fullName, Email email, Password password, Role role)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        Password = password;
        Role = role;
    }
    
    public static User Create(UserId id, FullName fullName, Email email, Password password, Role role)
    {
        var user = new User(id, fullName, email, password, role);
        
        //TODO: add event
        //user.Raise(new UserCreatedDomainEvent(Guid.NewGuid(), DateTime.UtcNow, id));
        
        return user;
    }
    
    public void Update(UpdateUserDto dto)
    {
        FullName = dto.FullName ?? FullName;
        Password = dto.Password ?? Password;
        Role = dto.Role ?? Role;
        
        //TODO: add event in case of role change
    }
    
    internal static User TempAdmin 
        => new User(
            new UserId(Guid.NewGuid()), 
            new FullName("Temporary", "Administrator"),
            Email.TempAdminEmail, 
            Password.TempAdminPassword, 
            Role.Administrator);
}