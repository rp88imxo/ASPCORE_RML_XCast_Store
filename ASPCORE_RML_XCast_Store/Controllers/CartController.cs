using Microsoft.AspNetCore.Mvc;
using RMLXCast.Core.Domain.Cart;
using RMLXCast.Database;
using RMLXCast.Services.Catalog;
using RMLXCast.Web.Services.Cart;
using RMLXCast.Web.ViewModels.Cart;

namespace RMLXCast.Web.Controllers
{
    [Route("api/cart")]
    public class CartApiController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ICartService cartService;
        private readonly IProductService productService;

        public CartApiController(
            ApplicationDbContext dbContext,
            ICartService cartService,
            IProductService productService)
        {
            this.dbContext = dbContext;
            this.cartService = cartService;
            this.productService = productService;
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
        public async Task<IActionResult> AddProductToCart([FromBody] CartProductViewModel cartProductViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad model");
            }

            var product =  await productService.GetProductByIdAsync(cartProductViewModel.Id, false);

            if (product == null || !product.Published)
            {
                return BadRequest("No such product");
            }

            var cartProduct = new CartProduct()
            {
                Id = cartProductViewModel.Id,
                Amount = cartProductViewModel.Amount,
            };

            cartService.AddProductToCart(HttpContext.Session, cartProduct);

            return Ok();
        }
    }
}
