using Microsoft.AspNetCore.Mvc;
using RMLXCast.Core.Domain.Catalog;
using RMLXCast.Services.Catalog;
using RMLXCast.Web.ViewModels;
using RMLXCast.Web.ViewModelsFactories.ProductFactory;

namespace RMLXCast.Web.Controllers
{
    public class AdminProductsController : Controller
    {
        private readonly IProductViewModelFactory productViewModelFactory;
        private readonly IProductService productService;

        public AdminProductsController(
            IProductViewModelFactory productViewModelFactory,
            IProductService productService)
        {
            this.productViewModelFactory = productViewModelFactory;
            this.productService = productService;
        }

        public IActionResult Products()
        {
            return View();
        }

        [HttpGet]
        [Route("{controller}/api/{action}")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await productService.GetAllProducts();

            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad Request!");
            }

            var product = new Product()
            {
                Name = productViewModel.Name,
                AdminComment = productViewModel.AdminComment,
                AllowCustomerReviews = productViewModel.AllowCustomerReviews,
                CreatedOnUtc = DateTime.UtcNow,
                UpdatedOnUtc = DateTime.UtcNow,
                FullDescription = productViewModel.FullDescription,
                ShortDescription = productViewModel.ShortDescription,
                OrderMinimumQuantity = productViewModel.OrderMinimumQuantity,
                OrderMaximumQuantity = productViewModel.OrderMaximumQuantity,
                Price = productViewModel.Price,
                Published = productViewModel.Published,
            };

            product.Stocks.Add(new Stock { 
                StockQuantity= productViewModel.StockQuantity,
            });

            await productService.CreateProductAsync(product);

            return Ok(productViewModel);
        }
    }
}
