using RMLXCast.Core.Domain.Catalog;
using RMLXCast.Web.ViewModels;
using RMLXCast.Web.ViewModels.Product_Category;

namespace RMLXCast.Web.ViewModelsFactories.ProductCategoryFactory
{
    public interface IProductCategoryFactory
    {
        Task<EditProductCategoryViewModel> CreateEditProductCategoryViewModelAsync(ProductCategory productCategory);
        Task<PagedProductCategoriesViewModel> CreatePagedProductCategoriesViewModelAsync(IList<ProductCategory> productCategories, int pageNumber, int pageSize, int totalProducts);
        Task<CreateProductCategoryViewModel> CreateProductCategoryViewModelAsync();
    }
}