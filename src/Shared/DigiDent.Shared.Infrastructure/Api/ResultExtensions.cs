using DigiDent.Shared.Kernel.ReturnTypes;
using Microsoft.AspNetCore.Http;

namespace DigiDent.Shared.Infrastructure.Api;

public static class ResultExtensions
{
    /// <summary>
    /// Processes the failure response of a <see cref="Result"/> and returns the appropriate <see cref="IResult"/>.
    /// </summary>
    /// <param name="result">The result to be processed.</param>
    /// <param name="onSuccess">The function to be executed if the result is a success.</param>
    /// <param name="onFailure">(Optional) The function to be executed if the result is a failure. The default is set to <see cref="CustomResults.ProcessFailureResponse"/>.</param>
    /// <typeparam name="R">The type of the result to be returned.</typeparam>
    /// <returns></returns>
    public static R Match<R>(
        this Result result,
        Func<R> onSuccess,
        Func<Result, R>? onFailure=null) 
        where R : IResult
    {
        return (R)(result.IsSuccess 
            ? onSuccess()
            : onFailure is null 
                ? result.ProcessFailureResponse()
                : onFailure(result));
    }
    
    /// <summary>
    /// Processes the failure response of a <see cref="Result"/> and returns the appropriate <see cref="IResult"/>.
    /// </summary>
    /// <param name="result">The result to be processed.</param>
    /// <param name="onSuccess">The function to be executed if the result is a success.</param>
    /// <param name="onFailure">(Optional) The function to be executed if the result is a failure. The default is set to <see cref="CustomResults.ProcessFailureResponse"/>.</param>
    /// <typeparam name="T">The type of the value the result holds.</typeparam>
    /// <typeparam name="R">The type of the result to be returned.</typeparam>
    /// <returns></returns>
    public static R Match<T, R>(
        this Result<T> result,
        Func<T, R> onSuccess,
        Func<Result, R>? onFailure=null) 
        where T : notnull
        where R : IResult
    {
        return (R)(result.IsSuccess 
            ? onSuccess(result.Value!)
            : onFailure is null 
                ? result.ProcessFailureResponse()
                : onFailure(result));
    }
}