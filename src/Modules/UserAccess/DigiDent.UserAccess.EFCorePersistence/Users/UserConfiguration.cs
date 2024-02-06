using DigiDent.UserAccess.Domain.Users;
using DigiDent.UserAccess.Domain.Users.ValueObjects;
using DigiDent.Shared.Infrastructure.EfCore.Configurations;
using DigiDent.Shared.Infrastructure.EfCore.Converters;
using DigiDent.Shared.Kernel.ValueObjects;
using DigiDent.UserAccess.Domain.Users.Services;
using DigiDent.UserAccess.EFCorePersistence.Constants;
using DigiDent.UserAccess.EFCorePersistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiDent.UserAccess.EFCorePersistence.Users;

public class UserConfiguration: AggregateRootConfiguration<User, UserId, Guid>
{
    protected override void ConfigureEntity(EntityTypeBuilder<User> builder)
    {
        builder
            .Property(u => u.FullName)
            .HasConversion(ValueObjectsConverters.FullNameConverter)
            .HasColumnName(ConfigurationConstants.FullNameColumnName);
        
        builder
            .Property(u => u.Email)
            .HasConversion(ValueObjectsConverters.EmailConverter);

        builder
            .HasIndex(u => u.Email, "IX_Users_Email")
            .IsUnique();
        
        builder
            .Property(u => u.PhoneNumber)
            .HasConversion(ValueObjectsConverters.PhoneNumberConverter);

        builder
            .Property(u => u.Password)
            .HasConversion(CommonConverters.PasswordConverter);

        builder.HasData(new List<User>{ UsersFactory.CreateTempUserAdmin() });
    }

    protected override void ConfigureAggregateRoot(EntityTypeBuilder<User> builder)
    {
    }
}