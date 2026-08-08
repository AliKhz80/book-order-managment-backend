using CatalogService.Application.Messaging;

namespace CatalogService.Application.UseCases.Book.OrderEvent.EventModels;

public record StockFailedEvent(long OrderId, long BookId, long Quantity, string Reason);