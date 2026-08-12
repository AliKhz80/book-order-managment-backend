using System.Text.Json;
using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using CatalogService.Entities;
using CatalogService.UseCases.Book;
using CatalogService.UseCases.Book.ViewModels;
using Marten;
using Microsoft.Extensions.Caching.Distributed;

namespace CatalogService.UseCases.Book.Queries.GetBookModelById;

public class GetBookByIdQueryHandler(
    IDocumentSession session,
    IDistributedCache cache) : IQueryHandler<GetBookByIdQuery, BookViewModel>
{
    public async Task<BookViewModel> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = BookCacheKeys.ById(request.Id);
        var cachedBook = await cache.GetStringAsync(cacheKey, cancellationToken);

        if (!string.IsNullOrWhiteSpace(cachedBook))
        {
            var cachedViewModel = JsonSerializer.Deserialize<BookViewModel>(cachedBook);

            if (cachedViewModel is not null)
            {
                return cachedViewModel;
            }
        }

        var existEntity = await session.LoadAsync<Entities.Book>(request.Id, cancellationToken)
            ?? throw new NotFoundException("Book", request.Id);

        BookViewModel viewModel = new(existEntity.Id, existEntity.Title, existEntity.Author, existEntity.Stock, existEntity.Price);

        await cache.SetStringAsync(
            cacheKey,
            JsonSerializer.Serialize(viewModel),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            },
            cancellationToken);

        return viewModel;
    }
}
