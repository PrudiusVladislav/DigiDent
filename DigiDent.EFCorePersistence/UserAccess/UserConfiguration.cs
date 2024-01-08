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
            fullName.Property(fn => fn.FirstName).HasColumnName("FirstName");
            fullName.Property(fn => fn.LastName).HasColumnName("LastName");
        });
        
        //TODO: Remake the configuration so use converter instead of the ownsone
        //https://stackoverflow.com/questions/71619005/ef-core-valueconverter-or-ownedtype-for-simple-valueobjects
        
        builder.Property(u => u.Email).HasConversion(
            email => email.Value,
            value => new Email(value));

        builder.Property(u => u.Password).HasConversion(
            password => password.PasswordHash,
            value => new Password(value));
        
        builder.Property(u => u.Role).HasConversion(
            r => r.ToString(),
            value => Enum.Parse<Role>(value));
    }

    protected override void ConfigureAggregateRoot(EntityTypeBuilder<User> builder)
    {
    }
}