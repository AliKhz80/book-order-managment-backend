using CatalogService.Application.Features.Book.ViewModels;
using MediatR;

namespace CatalogService.Application.Features.Book.Queries.GetBookModelById;

public record GetBooklByIdQuery(
    long Id
    ) : IRequest<BookViewModel>;
