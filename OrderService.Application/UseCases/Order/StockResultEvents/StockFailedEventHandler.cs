using MassTransit;
using OrderService.Domain.Events;
using OrderService.Domain.Enums;
using OrderService.Domain.Interfaces;

namespace OrderService.Application.UseCases.Order.StockResultEvents;

public class StockFailedEventHandler(
    IOrderRepository orderRepository,
    IOrderUnitOfWork unitOfWork) : IConsumer<StockFailedEvent>
{
    public async Task Consume(ConsumeContext<StockFailedEvent> context)
    {
        var @event = context.Message;
        var order = await orderRepository.GetByIdAsync(@event.OrderId, context.CancellationToken)
            ?? throw new InvalidOperationException($"Order {@event.OrderId} was not found.");

        order.Status = OrderStatus.Failed;

        await orderRepository.UpdateAsync(order, context.CancellationToken);
        await unitOfWork.CommitAsync(context.CancellationToken);
    }
}
