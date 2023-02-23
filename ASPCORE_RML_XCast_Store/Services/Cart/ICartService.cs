using RMLXCast.Core.Domain.Cart;
using RMLXCast.Web.ViewModels.Cart;

namespace RMLXCast.Web.Services.Cart
{
    public interface ICartService
    {
        void AddProductToCart(ISession session, CartProduct cartProduct);
        void ClearCart(ISession session);
        void DeleteProductFromCart(ISession session, SessionCartProductViewModel sessionCartProductViewModel);
        IList<CartProduct>? GetCartProducts(ISession session);
        void UpdateProductCart(ISession session, UpdateCartViewModel updateCartViewModel);
    }
}