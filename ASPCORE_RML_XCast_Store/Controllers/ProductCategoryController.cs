using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMLXCast.Core.Domain.Catalog;
using RMLXCast.Services.Catalog;
using RMLXCast.Services.Catalog.Category;
using RMLXCast.Web.ViewModels.Product_Category;
using RMLXCast.Web.ViewModelsFactories.ProductCategoryFactory;
using RMLXCast.Web.ViewModelsFactories.ProductFactory;
using System.Data;

namespace RMLXCast.Web.Controllers
{
    [Authorize(Roles = "admin,moderator")]
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

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var viewModel = await productCategoryFactory.CreateProductCategoryViewModelAsync();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var productCategory = new ProductCategory()
                {
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    Editable = true
                };

                await productCategoryService.CreateProductCategoryAsync(productCategory);

                return RedirectToAction("Categories", "ProductCategory");
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var idValue = id.Value;

            var product = await productCategoryService.GetProductCategoryByIdAsync(idValue);

            if (product == null)
            {
                return BadRequest();
            }

            var viewModel = await productCategoryFactory.CreateEditProductCategoryViewModelAsync(product);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProductCategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var productCategory = await productCategoryService.GetProductCategoryByIdAsync(viewModel.Id);

                if (productCategory == null)
                {
                    return BadRequest();
                }

                productCategory.Name = viewModel.Name;
                productCategory.Description = viewModel.Description;

                await productCategoryService.UpdateProductCategoryAsync(productCategory);

                return RedirectToAction("Categories", "ProductCategory");
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (id == null)
            {
                return BadRequest();
            }

            var result = await productCategoryService.DeleteProductCategoryByIdAsync(id.Value);

            if (!result)
            {
                return BadRequest();
            }

            return RedirectToAction("Categories", "ProductCategory");
        }
    }
}
