using CatalogService.Application.Messaging;

namespace CatalogService.Application.Features.Book.OrderEvent.EventModels
{
    public record OrderCreatedEvent(long OrderId , long BookId , long Quantity) : IIntegrationEvent;
}
