
using OrderService.Application.Messaging;

namespace OrderService.Application.Features.Orders.StockResultEvents.EventModels;

public record StockReservedEvent(long OrderId, long BookId, long Quantity) : IIntegrationEvent;
