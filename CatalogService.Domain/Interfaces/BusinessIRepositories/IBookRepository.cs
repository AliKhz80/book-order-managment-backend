using CatalogService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Domain.Interfaces.BusinessIRepositories
{
    public interface IBookRepository : IRepository<Book , long>
    {
    }
}
