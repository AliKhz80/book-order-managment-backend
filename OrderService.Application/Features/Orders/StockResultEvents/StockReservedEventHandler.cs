namespace OrderService.Application.Features.Orders.StockResultEvents;

using OrderService.Application.Features.Orders.StockResultEvents.EventModels;
using OrderService.Application.Messaging;
using OrderService.Domain.Enums;
using OrderService.Domain.Interfaces;

public class StockReservedEventHandler(
    IOrderRepository orderRepository,
    IOrderUnitOfWork unitOfWork) : IIntegrationEventHandler<StockReservedEvent>
{
    public async Task HandleAsync(StockReservedEvent @event, CancellationToken cancellationToken = default)
    {
        var order = await orderRepository.GetByIdAsync(@event.OrderId, cancellationToken)
            ?? throw new InvalidOperationException($"Order {@event.OrderId} was not found.");

        order.Status = OrderStatus.Confirmed;

        await orderRepository.UpdateAsync(order, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}
