using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

namespace Infrastructure.Persistence;

public class RabbitMqContext
{
    private readonly IConnection _connection;
    public readonly IModel _channel;
    public IModel Channel => _channel;
    public RabbitMqContext()
    {
        Console.WriteLine("Connect to queue.");
        var factory = new ConnectionFactory
        {
            HostName = Environment.GetEnvironmentVariable("MQ_HOST") ?? "localhost"
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    public void QueueDeclare(string queue)
    {
        _channel.QueueDeclare(
            queue: queue,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
    }

    public void Publish<T>(string queue, T data)
    {
        _channel.BasicPublish(
            exchange: string.Empty,
            routingKey: queue,
            basicProperties: null,
            body: Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data)));
    }
}
