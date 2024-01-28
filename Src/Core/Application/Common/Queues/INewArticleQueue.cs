namespace Application.Common.Queues;

public interface INewArticleQueue
{
    void Publish<T>(T data);
}
