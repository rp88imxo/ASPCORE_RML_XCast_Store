using Microsoft.AspNetCore.Mvc;
using RMLXCast.Core.Domain.Catalog;
using RMLXCast.Services.Catalog;
using RMLXCast.Web.ViewModels.Product;
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

        public async Task<IActionResult> Products(int? page)
        {
            // TODO: Move to the config later...
            var pageValue = page ?? 1;
            int defaultPageSize = 15;

            var products  = await productService.GetPagedProductsAsync(pageValue, defaultPageSize);
            var totalProductCount = await productService.GetTotalProductCountAsync();
            var model = productViewModelFactory.CreateProductPagedViewModel(products, pageValue, defaultPageSize, totalProductCount);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var model = await productViewModelFactory.GetCreateProductViewModelAsync();

			return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> EditProduct(int? id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("[Edit]: Model is not valid!");
            }

            var idValue = id!.Value;

            var product = await productService.GetProductByIdAsync(idValue, true);

            if (product == null)
            {
                return BadRequest("Specified product is missing!");
            }

            

            return View();
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAllProducts()
        //{
        //    var products = await productService.GetAllProductsAsync();

        //    return Ok(products);
        //}


        //[HttpPost]
        //public async Task<IActionResult> CreateProduct([FromBody] ProductViewModel productViewModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest("Bad Request!");
        //    }

        //    var product = new Product()
        //    {
        //        Name = productViewModel.Name,
        //        AdminComment = productViewModel.AdminComment,
        //        AllowCustomerReviews = productViewModel.AllowCustomerReviews,
        //        CreatedOnUtc = DateTime.UtcNow,
        //        UpdatedOnUtc = DateTime.UtcNow,
        //        FullDescription = productViewModel.FullDescription,
        //        ShortDescription = productViewModel.ShortDescription,
        //        OrderMinimumQuantity = productViewModel.OrderMinimumQuantity,
        //        OrderMaximumQuantity = productViewModel.OrderMaximumQuantity,
        //        Price = productViewModel.Price,
        //        Published = productViewModel.Published,
        //    };

        //    product.Stocks.Add(new Stock { 
        //        StockQuantity= productViewModel.StockQuantity,
        //    });

        //    await productService.CreateProductAsync(product);

        //    productViewModel.Id = product.Id;

        //    return Ok(productViewModel);
        //}

        // TEST ONE
        //[HttpPost]
        //public async Task<IActionResult> UpdateProduct([FromBody] ProductViewModel productViewModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest("Product Model Is not valid!");
        //    }

        //    var product = await productService.GetProductByIdAsync(productViewModel.Id, true);

        //    if (product == null)
        //    {
        //        return BadRequest("Missing Product!");
        //    }

        //    product.Name = productViewModel.Name;
        //    product.ShortDescription= productViewModel.ShortDescription;
        //    product.Price = productViewModel.Price;
        //    product.Published = productViewModel.Published;
        //    product.UpdatedOnUtc = DateTime.UtcNow;
        //    var stock = product.Stocks.FirstOrDefault();
        //    if (stock != null)
        //    {
        //        stock.StockQuantity = productViewModel.StockQuantity;
        //    }

        //    await productService.UpdateProductAsync(product);

        //    return Ok(productViewModel);
        //}

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id, int page = 1)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Product Model Is not valid!");
            }

            var result = await  productService.DeleteProductByIdAsync(id);

            if (result)
            {
                return RedirectToAction("Products", "AdminProducts", new { page });
            }
            else
            {
                return BadRequest("Failed to delete!");
            }
        }
    }
}
