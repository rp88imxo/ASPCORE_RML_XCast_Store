using RMLXCast.Core.Domain.Catalog;
using RMLXCast.Web.ViewModels;

namespace RMLXCast.Web.ViewModelsFactories.ProductCategoryFactory
{
    public interface IProductCategoryFactory
    {
        Task<PagedProductCategoriesViewModel> CreatePagedProductCategoriesViewModelAsync(IList<ProductCategory> productCategories, int pageNumber, int pageSize, int totalProducts);
    }
}