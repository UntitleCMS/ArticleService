using Infrastructure.Persistence;
using System.Text.Json;
using System.Text;
using RabbitMQ.Client;
using System.Collections.Concurrent;
using RabbitMQ.Client.Events;
using System.Threading.Channels;
using MongoDB.Driver.Core.Bindings;
using System.Threading;

namespace Infrastructure.Queues;

public class FollowingArticlesRequest
{
    private readonly RabbitMqContext _rabbitMqContext;
    private readonly string REQUEST_Q = "following/articles";
    private readonly string REPLY_Q = "following/articles/reply";
    private readonly ConcurrentDictionary<string, TaskCompletionSource<string>> callbackMapper = new();

    public FollowingArticlesRequest(RabbitMqContext rabbitMqContext)
    {
        _rabbitMqContext = rabbitMqContext;
        _rabbitMqContext.QueueDeclare(REPLY_Q);
        _rabbitMqContext.QueueDeclare(REQUEST_Q);

        ConsumeReply();
    }

    private void ConsumeReply()
    {
        var consumer = new EventingBasicConsumer(_rabbitMqContext.Channel);
        consumer.Received += (model, ea) =>
        {
            if (!callbackMapper.TryRemove(ea.BasicProperties.CorrelationId, out var tcs))
                return;
            var body = ea.Body.ToArray();
            var response = Encoding.UTF8.GetString(body);
            tcs.TrySetResult(response);
        };

        _rabbitMqContext.Channel.BasicConsume(
            consumer: consumer,
            queue: REPLY_Q,
            autoAck: true);
    }

    public Task<string> PublishAsync<T>(T data, CancellationToken cancellationToken = default)
    {
        var channel = _rabbitMqContext.Channel;
        IBasicProperties props = channel.CreateBasicProperties();
        var correlationId = Guid.NewGuid().ToString();
        props.CorrelationId = correlationId;
        props.ReplyTo = REPLY_Q;
        var messageBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data));
        var tcs = new TaskCompletionSource<string>();
        callbackMapper.TryAdd(correlationId, tcs);

        channel.BasicPublish(exchange: string.Empty,
                             routingKey: REQUEST_Q,
                             basicProperties: props,
                             body: messageBytes);

        cancellationToken.Register(() => callbackMapper.TryRemove(correlationId, out _));
        return tcs.Task;
    }
}
