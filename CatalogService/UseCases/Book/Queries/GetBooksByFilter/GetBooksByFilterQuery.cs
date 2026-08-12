using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination;
using CatalogService.UseCases.Book.ViewModels;

namespace CatalogService.UseCases.Book.Queries.GetBooksByFilter;

public enum OrderType
{
    Ascending,
    Descending
}

public record GetBooksByFilterQuery(
    string? Title,
    string? Author,
    int PageSize = 20,
    int PageNumber = 1,
    OrderType OrderType = OrderType.Ascending
) : IQuery<PaginatedResult<BookViewModel>>;
