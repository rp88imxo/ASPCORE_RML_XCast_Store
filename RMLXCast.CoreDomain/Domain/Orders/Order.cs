using RMLXCast.Core.Domain.Catalog;
using RMLXCast.Core.Domain.ShippmentAddress;
using RMLXCast.Core.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMLXCast.Core.Domain.Orders
{
    public class Order
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public Guid OrderGuid { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
