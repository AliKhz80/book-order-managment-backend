using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Domain.Interfaces
{
    public interface ICurrentUser
    {
        public string IPAddress { get; }
        public string UserName { get; }
    }
}
