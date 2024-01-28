using Application.Common.Queues;
using MongoDB.Bson;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Queues;

public class NewArticleBus : IDisposable, INewArticleQueue
{
    private IConnection _connection;
    private IModel _channel;
    public NewArticleBus()
    {
        Console.WriteLine("Connect to queue.");
        // todo: dynamic hostname with env
        var factory = new ConnectionFactory { HostName = Environment.GetEnvironmentVariable("MQ_HOST") ?? "localhost"};
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(
            queue: "article/new",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

    }

    public void Publish<T>(T data)
    {
        _channel.BasicPublish(
            exchange: string.Empty,
            routingKey: "article/new",
            basicProperties: null,
            body: Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data)));
    }

    public void Dispose()
    {
        _connection.Dispose();
        _channel.Dispose();
    }
}
