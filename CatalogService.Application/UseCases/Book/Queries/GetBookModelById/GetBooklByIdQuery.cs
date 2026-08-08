using CatalogService.Application.UseCases.Book.ViewModels;
using MediatR;

namespace CatalogService.Application.UseCases.Book.Queries.GetBookModelById;

public record GetBooklByIdQuery(
    long Id
    ) : IRequest<BookViewModel>;
