using CatalogService.Entities;
using CatalogService.UseCases.Book;
using CatalogService.UseCases.Book.OrderEvent.EventModels;
using Marten;
using Microsoft.Extensions.Caching.Distributed;

namespace CatalogService.UseCases.Book.OrderEvent;

public interface IIntegrationEventHandler<in TEvent>
{
    Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default);
}

public class OrderCreatedEventHandler(
    IDocumentSession session,
    IDistributedCache cache) : IIntegrationEventHandler<OrderCreatedEvent>
{
    public async Task HandleAsync(OrderCreatedEvent @event, CancellationToken cancellationToken = default)
    {
        var book = await session.LoadAsync<Entities.Book>(@event.BookId, cancellationToken);

        if (book is null)
        {
            return;
        }

        if (book.Stock < @event.Quantity)
        {
            return;
        }

        book.Stock -= @event.Quantity;

        session.Update(book);
        await session.SaveChangesAsync(cancellationToken);
        await cache.RemoveAsync(BookCacheKeys.ById(book.Id), cancellationToken);
    }
}
