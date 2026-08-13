using CatalogService.Entities;
using CatalogService.Events;
using CatalogService.UseCases.Book;
using Marten;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;

namespace CatalogService.UseCases.Book.OrderEvent;

public class OrderCreatedEventHandler(
    IDocumentSession session,
    IDistributedCache cache,
    IPublishEndpoint publishEndpoint) : IConsumer<OrderCreatedEvent>
{
    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var @event = context.Message;
        var book = await session.LoadAsync<Entities.Book>(@event.BookId, context.CancellationToken);

        if (book is null)
        {
            await publishEndpoint.Publish(new StockFailedEvent(@event.OrderId), context.CancellationToken);
            return;
        }

        if (book.Stock < @event.Quantity)
        {
            await publishEndpoint.Publish(new StockFailedEvent(@event.OrderId), context.CancellationToken);
            return;
        }

        book.Stock -= @event.Quantity;

        session.Update(book);
        await session.SaveChangesAsync(context.CancellationToken);
        await cache.RemoveAsync(BookCacheKeys.ById(book.Id), context.CancellationToken);

        await publishEndpoint.Publish(new StockReservedEvent(@event.OrderId), context.CancellationToken);
    }
}

