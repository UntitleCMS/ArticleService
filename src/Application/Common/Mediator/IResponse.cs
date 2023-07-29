namespace Application.Common.Mediator;

public interface IResponse
{

}

public interface IResponse<T> : IResponse
{
    public T? Data { get; set; }
    public string Message { get; set; }
    public bool IsSuccess { get; set; }
    public IList<Exception>? Errors { get; set; }
}
