using Microsoft.AspNetCore.Identity;
using RMLXCast.Core.Domain.Orders;
using RMLXCast.Core.Domain.ShippmentAddress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMLXCast.Core.Domain.User
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<Address> Addresses { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public DateTime RegistrationDateUtc { get; set; }
    }
}
