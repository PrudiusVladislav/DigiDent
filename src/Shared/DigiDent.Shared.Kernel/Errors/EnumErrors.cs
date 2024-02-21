using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.Shared.Kernel.Errors;

public static class EnumErrors
{
    public static Error IncorrectEnumValue<TEnum>(string value)
        where TEnum : struct, Enum
    {
        return new Error(
            ErrorType.Validation,
            typeof(TEnum).Name,
            $"Incorrect value '{value}' for enum {typeof(TEnum).Name}");
    }
}