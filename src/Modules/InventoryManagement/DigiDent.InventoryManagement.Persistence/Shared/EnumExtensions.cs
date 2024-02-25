namespace DigiDent.InventoryManagement.Persistence.Shared;

internal static class EnumExtensions
{
    /// <summary>
    /// Used to convert an int enum value to its string representation
    /// </summary>
    /// <param name="value"> The int enum value represented as a string </param>
    /// <typeparam name="TEnum"> The enum type </typeparam>
    /// <returns></returns>
    internal static string ToEnumName<TEnum>(this string value)
        where TEnum : struct, Enum
    {
        return Enum.Parse<TEnum>(value).ToString();
    }
}