using CatalogService.Application.UseCases.Book.Queries.GetBooksByFilter;
using CatalogService.Domain.Specification;

namespace CatalogService.Application.UseCases.Book.Specifications;

public class GetBooksByFilterSpecification : Specification<Domain.Entities.Book>
{
    public GetBooksByFilterSpecification(GetBooksByFilterQuery query)
    {
        if (query.Title != "" && query.Title != null)
        {
            AddCriteria(x => x.Title.Contains(query.Title));
        }

        if (query.Author != "" && query.Author != null)
        {
            AddCriteria(x => x.Author.Contains(query.Author));
        }


        AddPaging(query.PageSize, query.PageNumber);
    }
}
