using DigiDent.Domain.SharedKernel.Abstractions;
using DigiDent.Domain.SharedKernel.ReturnTypes;

namespace DigiDent.API.Extensions;

public static class FailureResultsMapper
{
    public static IResult MapToIResult(this Result result)
    {
        var firstError = result.Errors.FirstOrDefault();

        return firstError?.Type switch
        {
            ErrorType.Conflict => Results.Conflict(new {error = firstError.Message}),
            ErrorType.NotFound => Results.NotFound(new {error = firstError.Message}),
            ErrorType.Validation => Results.BadRequest(result.Errors
                .Select(e => new { error = e.Message })),
            ErrorType.Unauthorized => Results.Unauthorized(),
            _ => Results.BadRequest("An unexpected error occurred")
        };
    }
}