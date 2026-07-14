using OrderService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Models
{
    public class Order
    {
        public long Id { get; set; }
        public long BookId { get; set; }
        public long Quantity { get; set; }
        public OrderStatus Status { get; set; }


    }
}
