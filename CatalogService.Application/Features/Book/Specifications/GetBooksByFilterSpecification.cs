using CatalogService.Application.Features.Book.Queries.GetBooksByFilter;
using CatalogService.Domain.SpecificationConfig;

namespace CatalogService.Application.Features.Book.Specifications;

public class GetBooksByFilterSpecification : Specification<Domain.Models.Book>
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
