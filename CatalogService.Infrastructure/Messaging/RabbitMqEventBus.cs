using System.Text;
using System.Text.Json;
using CatalogService.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace CatalogService.Infrastructure.Messaging;

public class RabbitMqEventBus : IEventBus
{
    private readonly RabbitMqOptions _options;

    public RabbitMqEventBus(IConfiguration configuration)
    {
        _options = RabbitMqOptionsFactory.Create(configuration);
    }

    public Task PublishAsync<TMessage>(TMessage message, string routingKey, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var factory = RabbitMqConnectionFactory.Create(_options);

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.ExchangeDeclare(_options.ExchangeName, ExchangeType.Direct, durable: true);

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
        var properties = channel.CreateBasicProperties();
        properties.Persistent = true;
        properties.ContentType = "application/json";
        properties.Type = typeof(TMessage).Name;

        channel.BasicPublish(
            exchange: _options.ExchangeName,
            routingKey: routingKey,
            basicProperties: properties,
            body: body);

        return Task.CompletedTask;
    }
}
