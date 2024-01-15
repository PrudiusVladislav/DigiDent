using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DigiDent.EFCorePersistence.Shared;

/// <summary>
/// Contains EF Core converters for enumerations.
/// </summary>
public static class EnumerationsConverter
{
    /// <summary>
    /// Generic converter for enum to string conversion.
    /// </summary>
    /// <typeparam name="TEnum"> The type of the enum. </typeparam>
    /// <returns></returns>
    public static ValueConverter<TEnum, string> EnumToStringConverter<TEnum>()
        where TEnum : struct, Enum
    {
        return new ValueConverter<TEnum, string>(
            value => value.ToString(),
            value => Enum.Parse<TEnum>(value));
    }
}