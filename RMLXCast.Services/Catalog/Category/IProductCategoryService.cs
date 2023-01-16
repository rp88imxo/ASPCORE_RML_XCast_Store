using RMLXCast.Core.Domain.Catalog;

namespace RMLXCast.Services.Catalog.Category
{
    public interface IProductCategoryService
    {
        Task CreateProductCategoryAsync(ProductCategory productCategory);
        Task DeleteProductCategoryByIdAsync(int id);
        Task<IList<ProductCategory>> GetAllProductCategoriesAsync();
        Task<IList<ProductCategory>> GetPagedProductCategoriesAsync(int pageNumber, int pageSize);
        Task<int> GetTotalProductCountAsync();
        Task UpdateProductCategoryAsync(ProductCategory productCategory);
    }
}