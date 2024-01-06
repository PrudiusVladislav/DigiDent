using DigiDent.Domain.UserAccessContext.Users;
using DigiDent.Domain.UserAccessContext.Users.ValueObjects;
using DigiDent.EFCorePersistence.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.EFCorePersistence.UserAccess.Configurations;

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
            value => Password.Create(value).Value!);

        builder.HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId);
    }

    protected override void ConfigureAggregateRoot(EntityTypeBuilder<User> builder)
    {
    }
}