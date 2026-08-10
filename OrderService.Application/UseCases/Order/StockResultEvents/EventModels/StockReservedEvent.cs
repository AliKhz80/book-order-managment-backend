using OrderService.Application.Messaging;

namespace OrderService.Application.UseCases.Order.StockResultEvents.EventModels;

public record StockReservedEvent(long OrderId) : IIntegrationEvent;
