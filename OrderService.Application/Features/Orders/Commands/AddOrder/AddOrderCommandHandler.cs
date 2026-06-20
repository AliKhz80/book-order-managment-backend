using MediatR;
using OrderService.Application.Features.Orders.StockResultEvents.EventModels;
using OrderService.Domain.Enums;
using OrderService.Domain.Interfaces;
using OrderService.Domain.Models;

namespace OrderService.Application.Features.Orders.Commands.AddOrder;

public class AddOrderCommandHandler(
    IOrderRepository orderRepository,
    IOrderUnitOfWork unitOfWork,
    IEventBus eventBus) : IRequestHandler<AddOrderCommand, long>
{
    public async Task<long> Handle(AddOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order
        {
            BookId = request.BookId,
            Quantity = request.Quantity,
            Status = OrderStatus.Pending
        };

        await orderRepository.AddAsync(order, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        await eventBus.PublishAsync(
            new OrderCreatedEvent(order.Id, order.BookId, order.Quantity),
            "order.created",
            cancellationToken);

        return order.Id;
    }
}
