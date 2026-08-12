using CatalogService.Application.Common.QueryModels;
using CatalogService.Application.UseCases.Book.Specifications;
using CatalogService.Application.UseCases.Book.ViewModels;
using CatalogService.Domain.Interfaces;
using MediatR;

namespace CatalogService.Application.UseCases.Book.Queries.GetBooksByFilter;

public class GetBooksByFilterQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetBooksByFilterQuery, Paging<BookViewModel>>
{
    public async Task<Paging<BookViewModel>> Handle(GetBooksByFilterQuery request, CancellationToken cancellationToken)
    {
        var specification = new GetBooksByFilterSpecification(request);
        var (totalCount, data) = await unitOfWork.BookRepositoryQuery.ListAsync(specification, cancellationToken);

        var viewModel = data.Select(book =>
        {
            BookViewModel bookViewModel = new(book.Title, book.Author, book.Stock, book.Price);
            return bookViewModel;
        }).ToList();

        var pagedList = Paging<BookViewModel>.Create(request.PageSize, request.PageNumber, totalCount, viewModel);

        return pagedList;
    }
}
