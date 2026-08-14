using BuildingBlocks.CQRS;
using MassTransit;
using OrderService.Domain.Events;
using OrderService.Domain.Enums;
using OrderService.Domain.Interfaces;

namespace OrderService.Application.UseCases.Order.Commands.AddOrder;

public class AddOrderCommandHandler(
    IOrderUnitOfWork unitOfWork,
    IPublishEndpoint publishEndpoint) : ICommandHandler<AddOrderCommand, long>
{
    public async Task<long> Handle(AddOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Domain.Entities.Order
        {
            BookId = request.BookId,
            Quantity = request.Quantity,
            Status = OrderStatus.Pending
        };

        await unitOfWork.OrderRepositoryCommond.AddAsync(order, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        await publishEndpoint.Publish(
            new OrderCreatedEvent(order.Id, order.BookId, order.Quantity),
            cancellationToken);

        return order.Id;
    }
}
