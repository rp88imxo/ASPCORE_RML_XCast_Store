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

        // TEST ONE
        [HttpGet]
        [Route("{controller}/api/{action}")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await productService.GetAllProducts();

            return Ok(products);
        }

        // TEST ONE
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

            productViewModel.Id = product.Id;

            return Ok(productViewModel);
        }

        // TEST ONE
        [HttpPost]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Product Model Is not valid!");
            }

            var product = await productService.GetProductByIdAsync(productViewModel.Id, true);

            if (product == null)
            {
                return BadRequest("Missing Product!");
            }

            product.Name = productViewModel.Name;
            product.ShortDescription= productViewModel.ShortDescription;
            product.Price = productViewModel.Price;
            product.Published = productViewModel.Published;
            product.UpdatedOnUtc = DateTime.UtcNow;
            var stock = product.Stocks.FirstOrDefault();
            if (stock != null)
            {
                stock.StockQuantity = productViewModel.StockQuantity;
            }

            await productService.UpdateProductAsync(product);

            return Ok(productViewModel);
        }

        // TEST ONE
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Product Model Is not valid!");
            }

            var result = await  productService.DeleteProductByIdAsync(id);

            if (result)
            {
                return Ok("Successfully deleted");
            }
            else
            {
                return BadRequest("Failed to delete!");
            }
        }
    }
}
