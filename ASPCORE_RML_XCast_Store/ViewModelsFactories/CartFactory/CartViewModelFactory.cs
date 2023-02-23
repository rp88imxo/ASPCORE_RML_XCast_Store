using RMLXCast.Core.Domain.Cart;
using RMLXCast.Services.Catalog;
using RMLXCast.Web.ViewModels.Cart;

namespace RMLXCast.Web.ViewModelsFactories.CartFactory
{
    public class CartViewModelFactory : ICartViewModelFactory
    {
        private readonly IProductService productService;

        public CartViewModelFactory(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<CartViewModel> GetCartViewModelAsync(IList<CartProduct> cartProducts)
        {
            var cartProductViewModels = new List<CartProductViewModel>();

            foreach (var cartProduct in cartProducts)
            {
                var product = await productService.GetProductByIdAsync(cartProduct.Id, true);

                if (product == null || !product.Stocks.Any(x => x.StockQuantity > 0))
                {
                    continue;
                }

                var viewModel = new CartProductViewModel()
                {
                    Amount = cartProduct.Amount,
                    ProductId = cartProduct.Id,
                    ProductName = product.Name,
                    PricePerSlot = product.Price,
                    OrderMinimumQuantity= product.OrderMinimumQuantity,
                    OrderMaximumQuantity = product.OrderMaximumQuantity,
                };

                cartProductViewModels.Add(viewModel);
            }

            var cart = new CartViewModel() { CartProducts = cartProductViewModels };
            return cart;
        }
    }
}
