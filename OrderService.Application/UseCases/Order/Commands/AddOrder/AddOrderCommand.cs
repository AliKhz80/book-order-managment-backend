using MediatR;

namespace OrderService.Application.UseCases.Order.Commands.AddOrder;

public record AddOrderCommand(
    long BookId,
    long Quantity
) : IRequest<long>;
