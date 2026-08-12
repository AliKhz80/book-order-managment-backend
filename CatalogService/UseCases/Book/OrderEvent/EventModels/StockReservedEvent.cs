namespace CatalogService.UseCases.Book.OrderEvent.EventModels;

public record StockReservedEvent(long OrderId, long BookId, long Quantity);
