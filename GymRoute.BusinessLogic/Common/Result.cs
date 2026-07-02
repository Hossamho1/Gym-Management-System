namespace GymRoute.BusinessLogic.Common;

public sealed class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string Error { get; }
    public string? Field { get; }

    private Result(bool isSuccess, string error, string? field)
    {
        IsSuccess = isSuccess;
        Error = error;
        Field = field;
    }

    public static Result Success() => new(true, string.Empty, null);

    public static Result Failure(string error, string? field = null) =>
        new(false, error, field);
}

public sealed class Result<T>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public T? Value { get; }
    public string Error { get; }
    public string? Field { get; }

    private Result(bool isSuccess, T? value, string error, string? field)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
        Field = field;
    }

    public static Result<T> Success(T value) => new(true, value, string.Empty, null);

    public static Result<T> Failure(string error, string? field = null) =>
        new(false, default, error, field);
}
