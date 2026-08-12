using BuildingBlocks.CQRS;
using CatalogService.UseCases.Book.ViewModels;

namespace CatalogService.UseCases.Book.Queries.GetBookModelById;

public record GetBookByIdQuery(
    long Id
) : IQuery<BookViewModel>;
