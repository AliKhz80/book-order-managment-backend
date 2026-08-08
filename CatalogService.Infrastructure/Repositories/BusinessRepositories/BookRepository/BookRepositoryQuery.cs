using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces.Repositories.BusinessIRepositories.BookRepository;

namespace CatalogService.Infrastructure.Repositories.BusinessRepositories.BookRepository
{
    public class BookRepositoryQuery(CatalogDBContext dbContext) : RepositoryQuery<Book , long>(dbContext), IBookRepositoryQuery
    {



    }
}
