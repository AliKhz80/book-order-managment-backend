using OrderService.Application.Messaging;
using OrderService.Application.UseCases.Order.StockResultEvents.EventModels;
using OrderService.Domain.Enums;
using OrderService.Domain.Interfaces;

namespace OrderService.Application.UseCases.Order.StockResultEvents;

public class StockFailedEventHandler(
    IOrderRepository orderRepository,
    IOrderUnitOfWork unitOfWork) : IIntegrationEventHandler<StockFailedEvent>
{
    public async Task HandleAsync(StockFailedEvent @event, CancellationToken cancellationToken = default)
    {
        var order = await orderRepository.GetByIdAsync(@event.OrderId, cancellationToken)
            ?? throw new InvalidOperationException($"Order {@event.OrderId} was not found.");

        order.Status = OrderStatus.Failed;

        await orderRepository.UpdateAsync(order, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}
