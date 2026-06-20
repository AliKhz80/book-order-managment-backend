using CatalogService.Application.Messaging;

namespace CatalogService.Domain.IntegrationEvents;

public record StockFailedEvent(long OrderId, long BookId, long Quantity, string Reason);