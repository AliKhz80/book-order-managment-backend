using CatalogService.Application.UseCases.Book;
using CatalogService.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace CatalogService.Application.UseCases.Book.Commands.DeleteBook;

public class DeleteBookCommandHandler(
    IUnitOfWork unitOfWork,
    IDistributedCache cache) : IRequestHandler<DeleteBookCommand>
{
    public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await unitOfWork.BookRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new Exception("Data Not Found!");

        await unitOfWork.BookRepository.DeleteAsync(book, cancellationToken);
        await unitOfWork.CommitAsync();
        await cache.RemoveAsync(BookCacheKeys.ById(request.Id), cancellationToken);
    }
}
