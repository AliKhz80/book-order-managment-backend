namespace OrderService.Domain.Interfaces;

public interface IEventBus
{
    Task PublishAsync<TMessage>(TMessage message, string routingKey, CancellationToken cancellationToken = default);
}
