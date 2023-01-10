using RMLXCast.Core.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMLXCast.Core.Domain.Orders
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public int? ProductId { get; set; }
        public Product? Product { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
