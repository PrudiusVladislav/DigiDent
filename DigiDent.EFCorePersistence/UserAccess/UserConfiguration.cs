using DigiDent.Domain.UserAccessContext.Users;
using DigiDent.Domain.UserAccessContext.Users.ValueObjects;
using DigiDent.EFCorePersistence.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.EFCorePersistence.UserAccess;

public class UserConfiguration: AggregateRootConfiguration<UserId, Guid, User>
{
    protected override void ConfigureEntity(EntityTypeBuilder<User> builder)
    {
        builder.OwnsOne(u => u.FullName, fullName =>
        {
            fullName.Property(f => f.FirstName).HasColumnName("FirstName");
            fullName.Property(f => f.LastName).HasColumnName("LastName");
        });
        
        builder.OwnsOne(u => u.Email, email =>
            email.Property(e => e.Value).HasColumnName("Email"));

        builder.OwnsOne(u => u.Password, password => 
            password.Property(p => p.PasswordHash).HasColumnName("PasswordHash"));
        
        builder.Property(u => u.Role).HasConversion(
            r => r.ToString(),
            value => Enum.Parse<Role>(value));
    }

    protected override void ConfigureAggregateRoot(EntityTypeBuilder<User> builder)
    {
    }
}