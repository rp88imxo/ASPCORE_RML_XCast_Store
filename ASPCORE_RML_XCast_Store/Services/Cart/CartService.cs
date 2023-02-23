using Newtonsoft.Json;
using RMLXCast.Core.Domain.Cart;
using RMLXCast.Core.Domain.Catalog;
using RMLXCast.Web.ViewModels.Cart;

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

        public void DeleteProductFromCart(ISession session, SessionCartProductViewModel sessionCartProductViewModel)
        {
            var products = GetCartProducts(session);

            if (products == null)
            {
                return;
            }

            var product = products.FirstOrDefault(x => x.Id == sessionCartProductViewModel.Id);

            if (product == null)
            {
                return;
            }

            products.Remove(product);

            SaveCart(session, products);
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

        public void UpdateProductCart(ISession session, UpdateCartViewModel updateCartViewModel)
        {
            SaveCart(session, updateCartViewModel.SessionCartProducts
                .Select(x => new CartProduct()
            {
                Id = x.ProductId,
                Amount = x.Amount,
            })
                .ToList());
        }

        public void ClearCart(ISession session)
        {
            session.SetString("cart", string.Empty);
        }

        private void SaveCart(ISession session, IList<CartProduct> cartProducts)
        {
            var cartList = new List<CartProduct>(cartProducts);

            var cartSerialized = JsonConvert.SerializeObject(cartList);

            session.SetString("cart", cartSerialized);
        }
    }
}
