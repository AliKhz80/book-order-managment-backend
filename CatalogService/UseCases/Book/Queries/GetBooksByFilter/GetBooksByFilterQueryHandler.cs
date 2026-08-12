using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination;
using CatalogService.Entities;
using CatalogService.UseCases.Book.ViewModels;
using Marten;

namespace CatalogService.UseCases.Book.Queries.GetBooksByFilter;

public class GetBooksByFilterQueryHandler(IDocumentSession session)
    : IQueryHandler<GetBooksByFilterQuery, PaginatedResult<BookViewModel>>
{
    public async Task<PaginatedResult<BookViewModel>> Handle(GetBooksByFilterQuery request, CancellationToken cancellationToken)
    {
        var query = session.Query<Entities.Book>().AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Title))
        {
            query = query.Where(b => b.Title.Contains(request.Title));
        }

        if (!string.IsNullOrWhiteSpace(request.Author))
        {
            query = query.Where(b => b.Author.Contains(request.Author));
        }

        query = request.OrderType == OrderType.Ascending
            ? query.OrderBy(b => b.Title)
            : query.OrderByDescending(b => b.Title);

        var totalCount = await query.CountAsync(cancellationToken);

        var pageIndex = request.PageNumber > 0 ? request.PageNumber - 1 : 0;
        var data = await query
            .Skip(pageIndex * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var viewModels = data.Select(b => new BookViewModel(b.Id, b.Title, b.Author, b.Stock, b.Price));

        return new PaginatedResult<BookViewModel>(pageIndex, request.PageSize, totalCount, viewModels);
    }
}
