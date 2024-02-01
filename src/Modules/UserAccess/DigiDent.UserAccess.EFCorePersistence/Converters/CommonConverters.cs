using DigiDent.UserAccess.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DigiDent.UserAccess.EFCorePersistence.Converters;

public static class CommonConverters
{
    public static ValueConverter<Password, string> PasswordConverter =>
        new ValueConverter<Password, string>(
            password => password.PasswordHash,
            value => new Password(value));
}