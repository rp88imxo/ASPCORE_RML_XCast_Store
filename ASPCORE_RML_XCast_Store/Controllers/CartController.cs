using Microsoft.AspNetCore.Mvc;
using RMLXCast.Core.Domain.Cart;
using RMLXCast.Database;
using RMLXCast.Services.Catalog;
using RMLXCast.Web.Services.Cart;
using RMLXCast.Web.ViewModels.Cart;
using RMLXCast.Web.ViewModelsFactories.CartFactory;

namespace RMLXCast.Web.Controllers
{
    [Route("api/cart")]
    public class CartApiController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ICartService cartService;
        private readonly IProductService productService;
        private readonly ICartViewModelFactory cartViewModelFactory;

        public CartApiController(
            ApplicationDbContext dbContext,
            ICartService cartService,
            IProductService productService,
            ICartViewModelFactory cartViewModelFactory)
        {
            this.dbContext = dbContext;
            this.cartService = cartService;
            this.productService = productService;
            this.cartViewModelFactory = cartViewModelFactory;
        }

        [Route("GetCart")]
        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var cartProducts = cartService.GetCartProducts(HttpContext.Session);

            if (cartProducts == null)
            {
                return Ok();
            }

            var cartViewModel = await cartViewModelFactory.GetCartViewModelAsync(cartProducts);

            return Ok(cartViewModel);
        }

        [Route("GetCartProducts")]
        [HttpGet]
        public IActionResult GetCartProducts()
        {
            var cartProduct = cartService.GetCartProducts(HttpContext.Session);

            if (cartProduct == null)
            {
                return BadRequest("Can't get a cart product!");
            }
            else
            {
                return Ok(cartProduct);
            }
        }

        [Route("AddProductToCart")]
        [HttpPost]
        public async Task<IActionResult> AddProductToCart([FromBody] SessionCartProductViewModel cartProductViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad model");
            }

            var product =  await productService.GetProductByIdAsync(cartProductViewModel.Id, false);

            if (product == null || !product.Published 
                || cartProductViewModel.Amount <= 0 ||
                cartProductViewModel.Amount > product.OrderMaximumQuantity 
                || cartProductViewModel.Amount < product.OrderMinimumQuantity)
            {
                return BadRequest("Failed to add to the cart!");
            }

            var cartProduct = new CartProduct()
            {
                Id = cartProductViewModel.Id,
                Amount = cartProductViewModel.Amount,
            };

            cartService.AddProductToCart(HttpContext.Session, cartProduct);

            return Ok();
        }

        [Route("UpdateProductCart")]
        [HttpPost]
        public async Task<IActionResult> UpdateProductCart([FromBody] UpdateCartViewModel updateCartViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad model");
            }

            foreach (var sessionProduct in updateCartViewModel.SessionCartProducts)
            {
                var product = await productService.GetProductByIdAsync(sessionProduct.ProductId, false);

                if (product == null || !product.Published)
                {
                    cartService.ClearCart(HttpContext.Session);
                    return BadRequest("Failed to update cart!");
                }
            }

            cartService.UpdateProductCart(HttpContext.Session, updateCartViewModel);

            return Ok();
        }

        [Route("DeleteProductFromCart")]
        [HttpPost]
        public async Task<IActionResult> DeleteProductFromCart([FromBody] SessionCartProductViewModel cartProductViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad model");
            }

            var product = await productService.GetProductByIdAsync(cartProductViewModel.Id, false);

            if (product == null || !product.Published)
            {
                return BadRequest("Failed to delete product from the cart!");
            }

            cartService.DeleteProductFromCart(HttpContext.Session, cartProductViewModel);

            return Ok();
        }
    }
}
