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
        
        builder.Property(u => u.Email).HasConversion(
            email => email.Value,
            value => Email.Create(value).Value!);

        builder.Property(u => u.Password).HasConversion(
            password => password.PasswordHash,
            value => Password.CreateFromHash(value));
        
        builder.Property(u => u.Role).HasConversion(
            r => r.ToString(),
            value => Enum.Parse<Role>(value));
    }

    protected override void ConfigureAggregateRoot(EntityTypeBuilder<User> builder)
    {
    }
}