using DigiDent.Shared.Kernel.ReturnTypes;
using Microsoft.AspNetCore.Http;

namespace DigiDent.Shared.Infrastructure.Api;

public static class CustomResults
{
    private static readonly Dictionary<ErrorType, Func<List<Error>, IResult>> ErrorTypeToResultMap =
        new()
        {
           [ErrorType.Conflict] = errors => Results.Conflict(
               new {error = errors.First().Message}),
           
           [ErrorType.NotFound] = errors => Results.NotFound(
               new {error = errors.First().Message}),
           
           [ErrorType.Validation] = errors => Results.BadRequest(
               errors.Select(e => new { error = e.Message })),
           
           [ErrorType.Unauthorized] = _ => Results.Unauthorized(),
        };
    
    private static IResult UnexpectedError() => Results.BadRequest("An unexpected error occurred");
    
    /// <summary>
    /// Processes the failure response of a <see cref="Result"/> and returns the appropriate <see cref="IResult"/>.
    /// </summary>
    /// <param name="result">The result to be processed.</param>
    /// <returns></returns>
    public static IResult ProcessFailureResponse(this Result result)
    {
        Error? firstError = result.Errors.FirstOrDefault();

        if (firstError is null || !ErrorTypeToResultMap.TryGetValue(firstError.Type, out var resultFunc))
            return UnexpectedError();
        
        return resultFunc(result.Errors);
    }
}