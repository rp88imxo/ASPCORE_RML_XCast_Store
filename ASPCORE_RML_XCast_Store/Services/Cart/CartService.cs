using Newtonsoft.Json;
using RMLXCast.Core.Domain.Cart;
using RMLXCast.Core.Domain.Catalog;

namespace RMLXCast.Web.Services.Cart
{
    public class CartService : ICartService
    {
        public void AddProductToCart(ISession session, CartProduct cartProduct)
        {
            List<CartProduct>? cartList = null;

            var cart = session.GetString("cart");

            if (!string.IsNullOrEmpty(cart))
            {
                cartList = JsonConvert.DeserializeObject<List<CartProduct>>(cart);

                if (cartList == null)
                {
                    cartList = new List<CartProduct>
                {
                    cartProduct
                };
                }
                else
                {
                    var product = cartList.Find(x => x.Id == cartProduct.Id);

                    if (product != null)
                    {
                        product.Amount += cartProduct.Amount;
                    }
                    else
                    {
                        cartList.Add(cartProduct);
                    }
                }
            }
            else
            {
                cartList = new List<CartProduct>
                {
                    cartProduct
                };
            }


            var cartSerialized = JsonConvert.SerializeObject(cartList);

            session.SetString("cart", cartSerialized);
        }

        public IList<CartProduct>? GetCartProducts(ISession session)
        {
            var cartString = session.GetString("cart");

            if (cartString == null)
            {
                return null;
            }

            var cartProduct = JsonConvert.DeserializeObject<List<CartProduct>>(cartString);

            return cartProduct;
        }
    }
}
