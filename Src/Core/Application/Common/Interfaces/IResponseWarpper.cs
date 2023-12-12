
namespace Application.Common.Interfaces;

public interface IResponseWrapper
{
    public string Message { get; set; }
    public bool IsSuccess { get; set; }
    public Exception? Error { get; set; }
}

public interface IResponseWrapper<T> : IResponseWrapper
{
    public T? Data { get; set; }
}

public interface IResponsePageable<T>  
{
    public IEnumerable<T> Collections { get; set; }
    public string Pivot { get; set; }
    public bool HasNext { get; set; }
    public bool HasPrevious { get; set; }
    public long CountAll { get; set; }
    public int CountData { get; }
}
