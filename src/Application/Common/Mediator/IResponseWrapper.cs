namespace Application.Common.Mediator;

public interface IResponseWrapper
{

}

public interface IResponseWrapper<T> : IResponseWrapper
{
    public T? Data { get; set; }
    public string Message { get; set; }
    public bool IsSuccess { get; set; }
    public IList<Exception>? Errors { get; set; }
}
