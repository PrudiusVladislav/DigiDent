using DigiDent.Domain.UserAccessContext.Users;
using DigiDent.Domain.UserAccessContext.Users.ValueObjects;
using DigiDent.EFCorePersistence.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.EFCorePersistence.UserAccess.Users;

public class UserConfiguration: AggregateRootConfiguration<UserId, Guid, User>
{
    protected override void ConfigureEntity(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.FullName).HasConversion(
            fullName => fullName.ToString(),
            value => FullName.CreateFromNameParts(
                value.Split(' ', StringSplitOptions.None)))
            .HasColumnName("Full Name");
                
        
        builder.Property(u => u.Email).HasConversion(
            email => email.Value,
            value => new Email(value));

        builder.Property(u => u.Password).HasConversion(
            password => password.PasswordHash,
            value => new Password(value));
        
        builder.Property(u => u.Role).HasConversion(
            r => r.ToString(),
            value => Enum.Parse<Role>(value));

        builder.HasData(new List<User>{ User.TempAdmin });
    }

    protected override void ConfigureAggregateRoot(EntityTypeBuilder<User> builder)
    {
    }
}