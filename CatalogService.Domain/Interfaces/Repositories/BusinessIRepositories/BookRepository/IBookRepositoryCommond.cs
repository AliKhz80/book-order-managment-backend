using CatalogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Domain.Interfaces.Repositories.BusinessIRepositories.BookRepository
{
    public interface IBookRepositoryCommond : IRepositoryCommond<Book , long>
    {
    }
}
