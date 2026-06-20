using System.ComponentModel.DataAnnotations;
using MediatR;

namespace OrderService.Application.Features.Orders.Commands.AddOrder;

public record AddOrderCommand(
    long BookId,

    long Quantity
) : IRequest<long>;
