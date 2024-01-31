using DigiDent.Shared.Application.Abstractions;
using DigiDent.Shared.Domain.ReturnTypes;

namespace DigiDent.Shared.Application.Errors;

public static class CommandParametersErrors
{
    public static Error IncorrectParameter<TCommand>(string parameterName)
        => new (
            ErrorType.Validation,
            typeof(TCommand).Name,
            $"Incorrect value of parameter: {parameterName}.");
}