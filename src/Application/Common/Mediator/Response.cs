namespace Application.Common.Mediator;

public static class Response
{
    public static IResponseWrapper<T> Ok<T>(T a)
    {
        return new Response<T>()
        {
            Data = a,
            IsSuccess = true,
            Message = "OK"
        };
    }
    public static IResponseWrapper<T> Fail<T>(Exception a, T? data = default)
    {
        return new Response<T>()
        {
            Data = data,
            IsSuccess = false,
            Message = "Fail",
            Errors = new List<Exception>() { a }
        };
    }

}


public class Response<T> : IResponseWrapper<T>
{
    public T? Data { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool IsSuccess { get; set; }
    public IList<Exception>? Errors { get; set; }
}
