using CatalogService.Application.Common.CurrentUser;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces;
using CatalogService.Domain.Interfaces.Repositories.BusinessIRepositories.BookRepository;

namespace CatalogService.Infrastructure.Repositories.BusinessRepositories.BookRepository
{
    public class BookRepositoryCommond(CatalogDBContext dbContext , ICurrentUser currentUser) : RepositoryCommond<Book , long>(dbContext , currentUser), IBookRepositoryCommond
    {





    }
}
