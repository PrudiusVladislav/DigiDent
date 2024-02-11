namespace DigiDent.Shared.Kernel.ReturnTypes;

public class Result
{
    public bool IsSuccess { get; protected set; }
    public bool IsFailure => !IsSuccess;
    public List<Error> Errors { get; }

    public Result()
    {
        IsSuccess = true;
        Errors = new List<Error>();
    }
    
    protected Result(bool isSuccess, List<Error>? errors)
    {
        var hasErrors = errors is not null && errors.Count != 0;
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
    
    /// <summary>
    /// Merges multiple results into one. If any of the results is a failure, the merged result will be a failure.
    /// </summary>
    /// <param name="results">The results to be merged.</param>
    /// <returns></returns>
    public static Result Merge(params Result[] results)
    {
        var errors = new List<Error>();
        foreach (var result in results)
        {
            errors.AddRange(result.Errors);
        }
        return new Result(results.All(r => r.IsSuccess), errors);
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
    public T Match<T>(Func<T> onSuccess, Func<Result, T> onFailure)
    {
        return IsSuccess ? onSuccess() : onFailure(this);
    }
    
    /// <summary>
    /// Asynchronous version of <see cref="Match{T}"/>.
    /// </summary>
    /// <returns></returns>
    public async Task<T> MatchAsync<T>(Func<Task<T>> onSuccess, Func<IList<Error>, T> onFailure)
    {
        return IsSuccess ? await onSuccess() : onFailure(Errors);
    }
    
    /// <summary>
    /// Maps the result to a new result of a different type.
    /// </summary>
    /// <typeparam name="R">The type of the new result's value.</typeparam>
    /// <returns></returns>
    public Result<R> MapToType<R>()
    {
        return new Result<R>(IsSuccess, default, Errors);
    }
}

public class Result<T> : Result
{
    public T? Value { get; protected set;}
    
    protected internal Result(bool isSuccess, T? value, List<Error>? errors)
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
    
    public R Match<R>(Func<T, R> onSuccess, Func<Result, R> onFailure)
    {
        return IsSuccess ? onSuccess(Value!) : onFailure(this);
    }
    
    // public static implicit operator Result<T>(T value)
    // {
    //     return new Result<T>(true, value, null);
    // }
}