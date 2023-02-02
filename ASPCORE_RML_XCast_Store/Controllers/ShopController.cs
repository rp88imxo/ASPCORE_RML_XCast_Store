using Microsoft.AspNetCore.Mvc;
using RMLXCast.Core.Domain.Catalog;
using RMLXCast.Services.Catalog;
using RMLXCast.Services.Catalog.Category;
using RMLXCast.Web.ViewModels;
using RMLXCast.Web.ViewModelsFactories.ShopProducts;
using System.Diagnostics;

namespace RMLXCast.Web.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductCategoryService productCategoryService;
        private readonly IProductService productService;
        private readonly IShopProductViewModelFactory shopProductViewModelFactory;

        public ShopController(
            IProductCategoryService productCategoryService,
            IProductService productService,
            IShopProductViewModelFactory shopProductViewModelFactory
            )
        {
            this.productCategoryService = productCategoryService;
            this.productService = productService;
            this.shopProductViewModelFactory = shopProductViewModelFactory;
        }

        public async Task<IActionResult> Products(string? searchString, int? page, int? categoryId)
        {
            int currentPage = page != null ? Math.Max(0, page.Value) : 1;
            int defaultPageSize = 20;

            ProductCategory? productCategory = null;
            if (categoryId != null)
            {
                var categoryIdValue = categoryId.Value;

                productCategory = await productCategoryService.GetProductCategoryByIdAsync(categoryIdValue);

                if (productCategory == null)
                {
                    return BadRequest();
                }
            }

            var products = await productService.GetPagedProductsAsync(currentPage, defaultPageSize, searchString ?? string.Empty, productCategory);

            int totalProducts = productCategory != null ? await productService.GetTotalProductCountByCategoryAsync(productCategory.Id) 
                : await productService.GetTotalProductCountAsync();

            var productCategories = await productCategoryService.GetAllProductCategoriesAsync();

            var model = shopProductViewModelFactory.CreateShopProductsPagedViewModel(
                products,
                productCategories,
                currentPage,
                defaultPageSize,
                totalProducts,
                searchString);
            model.CategoryId = productCategory?.Id;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ProductDetail(int? productId)
        {
            if (productId == null)
            {
                return BadRequest();
            }

            var productIdValue = productId.Value;

            var product = await productService.GetProductByIdAsync(productIdValue, true);

            if (product == null)
            {
                return BadRequest();
            }

            var model = shopProductViewModelFactory.CreateShopProductDetailViewModel(product);

            return View(model);
        }

        public IActionResult ErrorNotFound()
        {
            return View();
        }
    }
}
