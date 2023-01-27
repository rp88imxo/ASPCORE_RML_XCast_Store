using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMLXCast.Core.Domain.Catalog;
using RMLXCast.Services.Catalog;
using RMLXCast.Services.Catalog.Category;
using RMLXCast.Web.Services.ProductImagesService;
using RMLXCast.Web.ViewModels.Product;
using RMLXCast.Web.ViewModelsFactories.ProductFactory;
using System.Data;

namespace RMLXCast.Web.Controllers
{
    [Authorize(Roles = "admin,moderator")]
    public class AdminProductsController : Controller
    {
        private readonly IProductViewModelFactory productViewModelFactory;
        private readonly IProductService productService;
        private readonly IProductCategoryService productCategoryService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IProductImagesService productImagesService;

        public AdminProductsController(
            IProductViewModelFactory productViewModelFactory,
            IProductService productService,
            IProductCategoryService productCategoryService,
            IWebHostEnvironment webHostEnvironment,
            IProductImagesService productImagesService)
        {
            this.productViewModelFactory = productViewModelFactory;
            this.productService = productService;
            this.productCategoryService = productCategoryService;
            this.webHostEnvironment = webHostEnvironment;
            this.productImagesService = productImagesService;
        }

        public async Task<IActionResult> Products(int? page)
        {
            // TODO: Move to the config later...
            var pageValue = page ?? 1;
            int defaultPageSize = 30;

            var products = await productService.GetPagedProductsAsync(pageValue, defaultPageSize);
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

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.SelectedProductCategoriesIds.Count == 0)
                {
                    ModelState.AddModelError("", "Товар должен иметь хотя бы одну категорию.");

                    await productViewModelFactory.UpdateCreateProductViewModelAsync(viewModel);
                    return View(viewModel);
                }

                var product = new Product()
                {
                    Name = viewModel.Name,
                    ShortDescription = viewModel.ShortDescription,
                    FullDescription = viewModel.FullDescription,
                    AdminComment = viewModel.AdminComment,
                    AllowCustomerReviews = viewModel.AllowCustomerReviews,
                    Price = viewModel.Price,
                    OrderMinimumQuantity = viewModel.OrderMinimumQuantity,
                    OrderMaximumQuantity = viewModel.OrderMaximumQuantity,
                    Published = viewModel.Published,
                    CreatedOnUtc = DateTime.UtcNow,
                    UpdatedOnUtc = DateTime.UtcNow,
                };

                product.Stocks.Add(new Stock
                {
                    StockQuantity = viewModel.Stock
                });

                foreach (var selectedCategory in viewModel.SelectedProductCategoriesIds)
                {
                    product.ProductProductCategories.Add(new ProductProductCategory { ProductCategoryId = selectedCategory });
                }

                await productService.CreateProductAsync(product);

                // TODO: Save product images

                await productImagesService.SaveProductImagesAsync(product, viewModel.ProductImages);

                return RedirectToAction("Products", "AdminProducts");
            }

            await productViewModelFactory.UpdateCreateProductViewModelAsync(viewModel);
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(int? id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("[EditProduct]: Model is not valid!");
            }

            var idValue = id!.Value;

            var product = await productService.GetProductByIdAsync(idValue, true);

            if (product == null)
            {
                return BadRequest("Specified product is missing!");
            }

            var model = await productViewModelFactory.GetEditProductViewModelAsync(product);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(EditProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.SelectedProductCategoriesIds.Count == 0)
                {
                    ModelState.AddModelError("", "Товар должен иметь хотя бы одну категорию.");

                    await productViewModelFactory.UpdateEditProductViewModelAsync(viewModel);
                    return View(viewModel);
                }

                var product = await productService.GetProductByIdAsync(viewModel.Id, true);

                if (product == null)
                {
                    return BadRequest("Missing product with such id");
                }

                product.Name = viewModel.Name;
                product.ShortDescription = viewModel.ShortDescription;
                product.FullDescription = viewModel.FullDescription;
                product.AdminComment = viewModel.AdminComment;
                product.AllowCustomerReviews = viewModel.AllowCustomerReviews;
                product.Price = viewModel.Price;
                product.OrderMinimumQuantity = viewModel.OrderMinimumQuantity;
                product.OrderMaximumQuantity = viewModel.OrderMaximumQuantity;
                product.Published = viewModel.Published;
                product.UpdatedOnUtc = DateTime.UtcNow;

                var stock = product.Stocks.FirstOrDefault();
                stock!.StockQuantity = viewModel.Stock;

                product.ProductProductCategories.Clear();
                foreach (var selectedCategory in viewModel.SelectedProductCategoriesIds)
                {
                    product.ProductProductCategories.Add(new ProductProductCategory { ProductCategoryId = selectedCategory });
                }

                await productService.UpdateProductAsync(product);

                await productImagesService.SaveProductImagesAsync(product, viewModel.ProductImages);

                return RedirectToAction("Products", "AdminProducts");
            }

            await productViewModelFactory.UpdateEditProductViewModelAsync(viewModel);
            return View(viewModel);
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

            var result = await productService.DeleteProductByIdAsync(id);

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
