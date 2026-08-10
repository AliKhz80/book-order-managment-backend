namespace OrderService.Application.UseCases.Order.StockResultEvents.EventModels;

public record OrderCreatedEvent(long OrderId, long BookId, long Quantity);
