using PagedList.Core;
using RMLXCast.Core.Domain.Catalog;
using RMLXCast.Web.ViewModels;
using RMLXCast.Web.ViewModels.Product_Category;

namespace RMLXCast.Web.ViewModelsFactories.ProductCategoryFactory
{
    public class ProductCategoryFactory : IProductCategoryFactory
    {
        public Task<PagedProductCategoriesViewModel> CreatePagedProductCategoriesViewModelAsync(
            IList<ProductCategory> productCategories,
            int pageNumber,
            int pageSize,
            int totalProducts)
        {
            var model = new PagedProductCategoriesViewModel()
            {
                PageNumber = pageNumber,
                PagedProductCategoryViewModels = new StaticPagedList<PagedProductCategoryViewModel>(productCategories.Select(x => new PagedProductCategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Editable= x.Editable,
                }), pageNumber, pageSize, totalProducts)
            };

            return Task.FromResult(model);
        }

        public Task<CreateProductCategoryViewModel> CreateProductCategoryViewModelAsync()
        {
            var model = new CreateProductCategoryViewModel();

            return Task.FromResult(model);
        }

        public Task<EditProductCategoryViewModel> CreateEditProductCategoryViewModelAsync(ProductCategory productCategory)
        {
            var model = new EditProductCategoryViewModel()
            {
                Id = productCategory.Id,
                Name = productCategory.Name,
                Description = productCategory.Description,
            };

            return Task.FromResult(model);
        }
    }
}
