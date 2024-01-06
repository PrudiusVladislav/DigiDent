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
    
    public static Result Ok()
    {
        return new Result(true, null);
    }
    
    public static Result<T> Ok<T>(T value)
    {
        return new Result<T>(true, value, null);
    }
    
    public static Result Fail(Error error)
    {
        return new Result(false, new List<Error> { error });
    }

    public static Result<T> Fail<T>(Error error)
    {
        return new Result<T>(false, default, new List<Error> { error });
    }
    
    public T Match<T>(Func<T> onSuccess, Func<IList<Error>, T> onFailure)
    {
        return IsSuccess ? onSuccess() : onFailure(Errors);
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
    /// <param name="error"></param>
    /// <returns></returns>
    public override Result<T> AddError(Error error)
    {
        if (IsSuccess)
            IsSuccess = false;
        Errors.Add(error);
        return this;
    }
    
    public R Match<R>(Func<T, R> onSuccess, Func<IList<Error>,R> onFailure)
    {
        return IsSuccess ? onSuccess(Value!) : onFailure(Errors);
    }
    
    public Result<R> MapToFailure<R>()
    {
        return new Result<R>(false, default, Errors);
    }
}