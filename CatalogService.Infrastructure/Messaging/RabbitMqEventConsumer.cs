namespace CatalogService.Infrastructure.Messaging;

using CatalogService.Application.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

public class RabbitMqEventConsumer<TEvent>(
    IConfiguration configuration,
    IEventDispatcher eventDispatcher,
    ILogger<RabbitMqEventConsumer<TEvent>> logger) : BackgroundService
    where TEvent : class, IIntegrationEvent
{
    private IConnection? _connection;
    private IModel? _channel;
    private string? _queueName;

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var options = RabbitMqOptionsFactory.Create(configuration);
        var factory = RabbitMqConnectionFactory.Create(options);
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        var eventName = typeof(TEvent).Name.Replace("Event", "");

        var routingKey = Regex.Replace(eventName, "(?<!^)([A-Z])", "-$1").ToLower()
                             .Replace("-", ".");

        _queueName = $"{options.QueuePrefix}.{routingKey.Replace(".", "-")}.queue";

        _channel.ExchangeDeclare(options.ExchangeName, ExchangeType.Direct, durable: true);
        _channel.QueueDeclare(_queueName, durable: true, exclusive: false, autoDelete: false);

        _channel.QueueBind(_queueName, options.ExchangeName, routingKey);

        _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.Received += async (_, eventArgs) => await HandleMessageAsync(eventArgs, stoppingToken);

        _channel.BasicConsume(_queueName, autoAck: false, consumer);

        logger.LogInformation("Started listening to queue: {QueueName} with routing key: {RoutingKey}",
            _queueName, routingKey);

        return Task.Delay(Timeout.Infinite, stoppingToken);
    }

    private async Task HandleMessageAsync(BasicDeliverEventArgs eventArgs, CancellationToken cancellationToken)
    {
        if (_channel is null) return;

        try
        {
            await ProcessMessageAsync(eventArgs, cancellationToken);
            _channel.BasicAck(eventArgs.DeliveryTag, multiple: false);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Processing failed for queue {QueueName}. Requeueing message.", _queueName);
            _channel.BasicNack(eventArgs.DeliveryTag, multiple: false, requeue: true);
        }
    }

    private async Task ProcessMessageAsync(BasicDeliverEventArgs eventArgs, CancellationToken cancellationToken)
    {
        var json = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

        // Dynamically deserialize into the generic TEvent type
        var @event = JsonSerializer.Deserialize<TEvent>(json)
            ?? throw new InvalidOperationException($"Message could not be deserialized into {typeof(TEvent).Name}.");

        // Dispatch dynamically 
        await eventDispatcher.DispatchAsync(@event, cancellationToken);
    }

    public override void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
        _channel?.Dispose();
        _connection?.Dispose();
        base.Dispose();
    }
}