using RMLXCast.Core.Domain.Catalog;

namespace RMLXCast.Services.Catalog.Category
{
    public interface IProductCategoryService
    {
        Task CreateProductCategoryAsync(ProductCategory productCategory);
        Task<bool> DeleteProductCategoryByIdAsync(int id);
        Task<IList<ProductCategory>> GetAllProductCategoriesAsync();
        Task<IList<ProductCategory>> GetPagedProductCategoriesAsync(int pageNumber, int pageSize);
        Task<ProductCategory?> GetProductCategoryByIdAsync(int id);
        Task<int> GetTotalProductCountAsync();
        Task UpdateProductCategoryAsync(ProductCategory productCategory);
    }
}