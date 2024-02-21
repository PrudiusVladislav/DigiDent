using DigiDent.Shared.Kernel.Errors;
using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.Shared.Kernel.Extensions;

public static class EnumExtensions
{
    public static Result<TEnum> ToEnum<TEnum>(this string value)
        where TEnum : struct, Enum
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Ok().MapToType<TEnum>();

        if (!Enum.TryParse<TEnum>(value, out var parsedValue))
        {
            return Result.Fail<TEnum>(EnumErrors
                .IncorrectEnumValue<TEnum>(value));
        }

        return Result.Ok(parsedValue);
    }
    
}