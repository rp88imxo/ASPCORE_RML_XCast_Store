using RMLXCast.Core.Domain.Cart;
using RMLXCast.Core.Domain.Catalog;
using RMLXCast.Core.Domain.Orders;
using RMLXCast.Core.Domain.ShippmentAddress;
using RMLXCast.Core.Domain.User;

namespace RMLXCast.Services.Orders.OrderService
{
    public interface IOrderService
    {
        Task CreateOrderAsync(IEnumerable<Product> products, IEnumerable<CartProduct> cartProducts, ApplicationUser applicationUser, Address address);
        Task<IList<Order>> GetAllOrdersForUserAsync(ApplicationUser user);
    }
}