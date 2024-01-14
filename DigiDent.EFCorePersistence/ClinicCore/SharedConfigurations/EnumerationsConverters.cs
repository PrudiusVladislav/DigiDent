using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DigiDent.EFCorePersistence.ClinicCore.SharedConfigurations;

public static class EnumerationsConverters
{
    public static ValueConverter<TEnum, string> EnumToStringConverter<TEnum>()
        where TEnum : struct, Enum
    {
        return new ValueConverter<TEnum, string>(
            value => value.ToString(),
            value => Enum.Parse<TEnum>(value));
    }
}