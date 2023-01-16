using PagedList.Core;
using RMLXCast.Core.Domain.Catalog;
using RMLXCast.Web.ViewModels;

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
                }), pageNumber, pageSize, totalProducts)
            };

            return Task.FromResult(model);
        }


    }
}
