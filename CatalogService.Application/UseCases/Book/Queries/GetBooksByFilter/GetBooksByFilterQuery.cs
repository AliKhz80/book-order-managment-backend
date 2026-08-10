using CatalogService.Application.Common.QueryModels;
using CatalogService.Application.UseCases.Book.ViewModels;
using MediatR;

namespace CatalogService.Application.UseCases.Book.Queries.GetBooksByFilter;

public record GetBooksByFilterQuery(
    string? Title,
    string? Author,
    int PageSize = 20,
    int PageNumber = 1,
    OrderType OrderType = OrderType.Ascending
    ) : IRequest<Paging<BookViewModel>>;

