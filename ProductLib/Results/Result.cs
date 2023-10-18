namespace ProductLib;

public class Result<T>
{
    public bool Succeded { get; set; } = default;
    public string? Message { get; set; } = default;
    public T? Data { get; set; } = default;

    public static Result<T> Success(T? data, string? message = null)
    {
        return new Result<T>()
        {
            Succeded = true,
            Message = message,
            Data = data,
        };
    }
    public static Result<T> Fail(string? message = null)
    {
        return new Result<T>()
        {
            Succeded=false,
            Message = message,
            Data = default 
        };
    }
}