using RMLXCast.Core.Domain.Cart;

namespace RMLXCast.Web.Services.Cart
{
    public interface ICartService
    {
        void AddProductToCart(ISession session, CartProduct cartProduct);
        IList<CartProduct>? GetCartProducts(ISession session);
    }
}