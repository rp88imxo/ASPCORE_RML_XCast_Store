using Microsoft.AspNetCore.Mvc;
using RMLXCast.Core.Domain.Cart;
using RMLXCast.Database;
using RMLXCast.Web.Services.Cart;
using RMLXCast.Web.ViewModels.Cart;

namespace RMLXCast.Web.Controllers
{
    [Route("api/cart")]
    public class CartApiController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ICartService cartService;

        public CartApiController(
            ApplicationDbContext dbContext,
            ICartService cartService)
        {
            this.dbContext = dbContext;
            this.cartService = cartService;
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
        public IActionResult AddProductToCart([FromBody] CartProductViewModel cartProductViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad model");
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
