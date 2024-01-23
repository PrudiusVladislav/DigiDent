using DigiDent.Application.Shared.Abstractions;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.Application.Shared.Errors;

public static class CommandParametersErrors
{
    public static Error IncorrectParameter<TCommand>(string parameterName)
        => new (
            ErrorType.Validation,
            typeof(TCommand).Name,
            $"Incorrect value of parameter: {parameterName}.");
}