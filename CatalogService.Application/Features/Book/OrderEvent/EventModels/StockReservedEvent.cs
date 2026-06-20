
namespace CatalogService.Domain.IntegrationEvents;

public record StockReservedEvent(long OrderId, long BookId, long Quantity);
