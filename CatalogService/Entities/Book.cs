using CatalogService.Domain.Entities.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Domain.Entities
{
    public class Book : TrackableEntity<long>
    {
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public long Stock { get; set; }
        public int Price { get; set; }

    }
}
