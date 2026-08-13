using BuildingBlocks.CQRS;
using MassTransit;
using MediatR;
using OrderService.Domain.Events;
using OrderService.Domain.Entities;
using OrderService.Domain.Enums;
using OrderService.Domain.Interfaces;

namespace OrderService.Application.UseCases.Order.Commands.AddOrder;

public class AddOrderCommandHandler(
    IOrderRepository orderRepository,
    IOrderUnitOfWork unitOfWork,
    IPublishEndpoint publishEndpoint) : ICommandHandler<AddOrderCommand, long>
{
    public async Task<long> Handle(AddOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new OrderService.Domain.Entities.Order
        {
            BookId = request.BookId,
            Quantity = request.Quantity,
            Status = OrderStatus.Pending
        };

        await orderRepository.AddAsync(order, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        await publishEndpoint.Publish(
            new OrderCreatedEvent(order.Id, order.BookId, order.Quantity),
            cancellationToken);

        return order.Id;
    }
}
