using CatalogService.Application.UseCases.Book;
using CatalogService.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace CatalogService.Application.UseCases.Book.Commands.UpdateBook;

public class UpdateBookCommandHandler(
    IUnitOfWork unitOfWork,
    IDistributedCache cache) : IRequestHandler<UpdateBookCommand>
{
    public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await unitOfWork.BookRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new Exception("Data Not Found!");

        book.Title = request.Title;
        book.Author = request.Author;
        book.Stock = request.Stock;
        book.Price = request.Price;

        await unitOfWork.BookRepository.UpdateAsync(book, cancellationToken);
        await unitOfWork.CommitAsync();
        await cache.RemoveAsync(BookCacheKeys.ById(request.Id), cancellationToken);
    }
}
