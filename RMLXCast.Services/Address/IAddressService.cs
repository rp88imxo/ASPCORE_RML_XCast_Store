using RMLXCast.Core.Domain.ShippmentAddress;

namespace RMLXCast.Services.AddressService
{
    public interface IAddressService
    {
        Task<Address> CreateAddressAsync(Address address);
        Task<Address?> GetAddressByIdAsync(int id);
    }
}