using RMLXCast.Core.Domain.Orders;
using RMLXCast.Core.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMLXCast.Core.Domain.ShippmentAddress
{
    public class Address
    {
        public int Id { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Address1 { get; set; }

        public string? Address2 { get; set; }

        public string? ZipPostalCode { get; set; }

        public bool DeletedByUser { get; set; }

        // One Address can have many orders
        public IEnumerable<Order> Orders { get; set; }
    }
}
