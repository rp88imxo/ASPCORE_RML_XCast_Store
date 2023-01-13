using RMLXCast.Core.Domain.Catalog;

namespace RMLXCast.Services.Catalog.Category
{
    public interface IProductCategoryService
    {
        Task<IList<ProductCategory>> GetAllProductCategoriesAsync();
    }
}