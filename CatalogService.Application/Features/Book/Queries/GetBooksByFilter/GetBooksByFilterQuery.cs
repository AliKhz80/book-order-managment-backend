using CatalogService.Application.Extentions.Models;
using CatalogService.Application.Features.Book.ViewModels;
using CatalogService.Domain.Enums;
using MediatR;

namespace CatalogService.Application.Features.Book.Queries.GetBooksByFilter;

public record GetBooksByFilterQuery(
    string? Title,
    string? Author,
    int PageSize = 20,
    int PageNumber = 1,
    OrderType OrderType = OrderType.Ascending
    ) : IRequest<Paging<BookViewModel>>;

