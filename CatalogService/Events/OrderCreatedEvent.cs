namespace CatalogService.Events;

public record OrderCreatedEvent(long OrderId, long BookId, long Quantity);
