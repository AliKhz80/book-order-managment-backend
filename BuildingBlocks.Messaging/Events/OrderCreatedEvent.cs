namespace OrderService.Domain.Events;

public record OrderCreatedEvent(long OrderId, long BookId, long Quantity);
