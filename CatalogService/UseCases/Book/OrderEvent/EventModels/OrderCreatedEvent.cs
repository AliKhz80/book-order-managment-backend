namespace CatalogService.UseCases.Book.OrderEvent.EventModels;

public record OrderCreatedEvent(long OrderId, long BookId, long Quantity);
