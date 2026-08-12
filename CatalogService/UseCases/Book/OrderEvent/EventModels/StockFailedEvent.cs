namespace CatalogService.UseCases.Book.OrderEvent.EventModels;

public record StockFailedEvent(long OrderId, long BookId, long Quantity, string Reason);