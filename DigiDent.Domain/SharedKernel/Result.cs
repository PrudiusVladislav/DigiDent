namespace DigiDent.Domain.SharedKernel;

public class Result
{
    public bool IsSuccess { get; protected set; }
    public bool IsFailure => !IsSuccess;
    public IList<Error> Errors { get; }

    public Result()
    {
        Errors = new List<Error>();
    }
    
    protected Result(bool isSuccess, IList<Error>? errors)
    {
        var hasErrors = errors is not null && errors.Any();
        if (isSuccess && hasErrors ||
            !isSuccess && !hasErrors)
        {
            throw new InvalidOperationException();
        }
        IsSuccess = isSuccess;
        Errors = errors ?? new List<Error>();
    }
    
    public virtual Result AddError(Error error)
    {
        if (IsSuccess)
            IsSuccess = false;
        Errors.Add(error);
        return this;
    }
    
    public void SetSuccess()
    {
        if (IsFailure)
            IsSuccess = true;
    }
    
    /// <summary>
    /// Merges multiple results into one. If any of the results is a failure, the merged result will be a failure.
    /// </summary>
    /// <param name="results">The results to be merged.</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Result<T> Merge<T>(params Result[] results)
    {
        var errors = new List<Error>();
        foreach (var result in results)
        {
            errors.AddRange(result.Errors);
        }
        return new Result<T>(results.All(r => r.IsSuccess), default, errors);
    }
    
    public static implicit operator bool(Result result) => result.IsSuccess;
    
    /// <summary>
    /// Creates a new <see cref="Result"/> with a success status.
    /// </summary>
    /// <returns></returns>
    public static Result Ok()
    {
        return new Result(true, null);
    }
    
    /// <summary>
    /// Creates a new <see cref="Result{T}"/> with a success status and a value.
    /// </summary>
    /// <param name="value"> The value to be stored in the result.</param>
    /// <typeparam name="T">The type of the result.</typeparam>
    /// <returns></returns>
    public static Result<T> Ok<T>(T value)
    {
        return new Result<T>(true, value, null);
    }
    
    /// <summary>
    /// Creates a new <see cref="Result"/> with a failure status and an error.
    /// </summary>
    /// <param name="error">The error to be stored in the result. Instance of <see cref="Error"/>.</param>
    /// <returns></returns>
    public static Result Fail(Error error)
    {
        return new Result(false, new List<Error> { error });
    }

    /// <summary>
    /// Overload of <see cref="Fail(Error)"/> that returns a <see cref="Result{T}"/> instead of a <see cref="Result"/>. 
    /// </summary>
    /// <param name="error">The error to be stored in the result. Instance of <see cref="Error"/>.</param>
    /// <typeparam name="T">The type of the result.</typeparam>
    /// <returns></returns>
    public static Result<T> Fail<T>(Error error)
    {
        return new Result<T>(false, default, new List<Error> { error });
    }
    
    /// <summary>
    /// Returns the result of one of the two functions depending on the value of <see cref="Result.IsSuccess"/>.
    /// </summary>
    /// <param name="onSuccess">
    /// The function to be executed if <see cref="Result.IsSuccess"/> is set to true.
    /// </param>
    /// <param name="onFailure">
    /// The function to be executed if <see cref="Result.IsSuccess"/> is set to false.
    /// </param>
    /// <typeparam name="T">The type of the result.</typeparam>
    /// <returns></returns>
    public T Match<T>(Func<T> onSuccess, Func<IList<Error>, T> onFailure)
    {
        return IsSuccess ? onSuccess() : onFailure(Errors);
    }
    
    /// <summary>
    /// Asynchronous version of <see cref="Match{T}(System.Func{T},System.Func{System.Collections.Generic.IList{Error},T})"/>. 
    /// </summary>
    /// <returns></returns>
    public async Task<Result<T>> MatchAsync<T>(
        Func<Task<Result<T>>> onSuccess,
        Func<IList<Error>, Result<T>> onFailure)
    {
        return IsSuccess ? await onSuccess() : onFailure(Errors);
    }
}

public class Result<T> : Result
{
    public T? Value { get; protected set;}
    
    protected internal Result(bool isSuccess, T? value, IList<Error>? errors)
        : base(isSuccess, errors)
    {
        Value = value;
    }

    public Result(): base()
    {
        Value = default;
    }
    
    /// <summary>
    /// Adds an error to the result and sets the result to failure. Returns the Result for chaining.
    /// </summary>
    /// <param name="error"> The error to be added. Instance of <see cref="Error"/>.</param>
    /// <returns></returns>
    public override Result<T> AddError(Error error)
    {
        if (IsSuccess)
            IsSuccess = false;
        Errors.Add(error);
        return this;
    }
    
    public R Match<R>(Func<T, R> onSuccess, Func<IList<Error>, R> onFailure)
    {
        return IsSuccess ? onSuccess(Value!) : onFailure(Errors);
    }
    
    public Result<R> MapToFailure<R>()
    {
        return new Result<R>(false, default, Errors);
    }
}