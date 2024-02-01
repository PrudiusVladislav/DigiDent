using DigiDent.Shared.Kernel.ReturnTypes;

namespace DigiDent.Shared.Abstractions.Errors;

public static class CommandParametersErrors
{
    public static Error IncorrectParameter<TCommand>(string parameterName)
        => new (
            ErrorType.Validation,
            typeof(TCommand).Name,
            $"Incorrect value of parameter: {parameterName}.");
}