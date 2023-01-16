using Microsoft.AspNetCore.Mvc;
using RMLXCast.Services.Catalog;
using RMLXCast.Services.Catalog.Category;
using RMLXCast.Web.ViewModelsFactories.ProductCategoryFactory;
using RMLXCast.Web.ViewModelsFactories.ProductFactory;

namespace RMLXCast.Web.Controllers
{
    public class ProductCategoryController : Controller
    {
        private readonly IProductCategoryFactory productCategoryFactory;
        private readonly IProductCategoryService productCategoryService;

        public ProductCategoryController(
            IProductCategoryFactory productCategoryFactory,
            IProductCategoryService productCategoryService)
        {
            this.productCategoryFactory = productCategoryFactory;
            this.productCategoryService = productCategoryService;
        }

        public async Task<IActionResult> Categories(int? page)
        {
            // TODO: Move to the config later...
            var pageValue = page ?? 1;
            int defaultPageSize = 30;

            var PagedProductCategories = await productCategoryService.GetPagedProductCategoriesAsync(pageValue, defaultPageSize);
            var totalProductCategoriesCount = await productCategoryService.GetTotalProductCountAsync();
            var model = await productCategoryFactory.CreatePagedProductCategoriesViewModelAsync(PagedProductCategories, pageValue, defaultPageSize, totalProductCategoriesCount);

            return View(model);
        }
    }
}
