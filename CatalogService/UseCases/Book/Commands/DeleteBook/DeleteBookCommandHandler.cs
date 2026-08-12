using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using CatalogService.Entities;
using CatalogService.UseCases.Book;
using Marten;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace CatalogService.UseCases.Book.Commands.DeleteBook;

public class DeleteBookCommandHandler(
    IDocumentSession session,
    IDistributedCache cache) : ICommandHandler<DeleteBookCommand>
{
    public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await session.LoadAsync<Entities.Book>(request.Id, cancellationToken)
            ?? throw new NotFoundException("Book", request.Id);

        session.Delete(book);
        await session.SaveChangesAsync(cancellationToken);

        await cache.RemoveAsync(BookCacheKeys.ById(request.Id), cancellationToken);

        return Unit.Value;
    }
}
