using DigiDent.Domain.SharedKernel;

namespace DigiDent.API.Extensions;

public static class FailureResultsMapper
{
    public static IResult MapFailureToIResult(this Result result)
    {
        var firstError = result.Errors.FirstOrDefault();

        return firstError?.Type switch
        {
            ErrorType.Conflict => Results.Conflict(firstError.Message),
            ErrorType.NotFound => Results.NotFound(firstError.Message),
            ErrorType.Validation => Results.BadRequest(result.Errors
                .Select(e => new { errorType = e.Type.ToString(), errorMessage = e.Message })),
            ErrorType.Unauthorized => Results.Unauthorized(),
            _ => Results.BadRequest("An unexpected error occurred")
        };
    }
}