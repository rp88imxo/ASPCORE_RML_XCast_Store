using Microsoft.EntityFrameworkCore;
using RMLXCast.Core.Domain.ShippmentAddress;
using RMLXCast.Core.Domain.User;
using RMLXCast.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMLXCast.Services.AddressService
{
    public class AddressService : IAddressService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public AddressService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<Address> CreateAddressAsync(Address address)
        {
            await applicationDbContext.Addresses.AddAsync(address);
            await applicationDbContext.SaveChangesAsync();
            return address;
        }

        public async Task<Address?> GetAddressByIdAsync(int id)
        {
            var res = await applicationDbContext.Addresses.FirstOrDefaultAsync(x => x.Id == id);
            return res;
        }
    }
}
