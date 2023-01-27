using RMLXCast.Core.Domain.Catalog;

namespace RMLXCast.Services.Catalog
{
    public interface IProductService
    {
        Task CreateProductAsync(Product product);
        Task<Product?> GetProductByIdAsync(int productId, bool includeStocks);
        Task UpdateProductAsync(Product product);
        Task<bool> DeleteProductByIdAsync(int productId);
        Task<IList<Product>> GetAllProductsAsync();
        Task<IList<Product>> GetPagedProductsAsync(int pageNumber, int pageSize);
        Task<int> GetTotalProductCountAsync();
        Task<IList<Product>> GetPagedProductsAsync(int pageNumber, int pageSize, string searchString, ProductCategory? productCategory, bool publishedOnly = true);
        Task<int> GetTotalProductCountByCategoryAsync(int productCategoryId);
    }
}