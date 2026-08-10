using OrderService.Application.Messaging;

namespace OrderService.Application.UseCases.Order.StockResultEvents.EventModels;

public record StockFailedEvent(long OrderId, string Reason) : IIntegrationEvent;
