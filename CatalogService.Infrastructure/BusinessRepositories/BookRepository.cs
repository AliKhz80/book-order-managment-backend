using CatalogService.Domain.Interfaces;
using CatalogService.Domain.Interfaces.BusinessIRepositories;
using CatalogService.Domain.Models;

namespace CatalogService.Infrastructure.BusinessRepositories
{
    public class BookRepository(CatalogDBContext dbContext , ICurrentUser currentUser) : Repository<Book , long>(dbContext , currentUser), IBookRepository
    {





    }
}
