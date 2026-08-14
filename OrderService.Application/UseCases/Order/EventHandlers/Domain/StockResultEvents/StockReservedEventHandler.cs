using MassTransit;
using OrderService.Domain.Events;
using OrderService.Domain.Enums;
using OrderService.Domain.Interfaces;

namespace OrderService.Application.UseCases.Order.EventHandlers.Domain.StockResultEvents;

public class StockReservedEventHandler(IOrderUnitOfWork unitOfWork) : IConsumer<StockReservedEvent>
{
    public async Task Consume(ConsumeContext<StockReservedEvent> context)
    {
        var @event = context.Message;
        var order = await unitOfWork.OrderRepositoryQuery.GetByIdAsync(@event.OrderId, context.CancellationToken)
            ?? throw new InvalidOperationException($"Order {@event.OrderId} was not found.");

        order.Status = OrderStatus.Confirmed;

        await unitOfWork.OrderRepositoryCommond.UpdateAsync(order, context.CancellationToken);
        await unitOfWork.CommitAsync(context.CancellationToken);
    }
}
