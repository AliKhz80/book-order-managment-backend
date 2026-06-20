using System.Text.Json;
using CatalogService.Application.Features.Book;
using CatalogService.Application.Features.Book.ViewModels;
using CatalogService.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace CatalogService.Application.Features.Book.Queries.GetBookModelById;

public class GetBookByIdQueryHandler(
    IUnitOfWork unitOfWork,
    IDistributedCache cache) : IRequestHandler<GetBooklByIdQuery, BookViewModel>
{
    public async Task<BookViewModel> Handle(GetBooklByIdQuery request, CancellationToken cancellationToken)
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

        var existEntity = await unitOfWork.BookRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new Exception("Data Not Found!");

        BookViewModel viewModel = new(existEntity.Title, existEntity.Author, existEntity.Stock, existEntity.Price);

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
