
using OrderService.Application.Messaging;

namespace OrderService.Application.Features.Orders.StockResultEvents.EventModels;

public record StockFailedEvent(long OrderId, long BookId, long Quantity, string Reason) :IIntegrationEvent;