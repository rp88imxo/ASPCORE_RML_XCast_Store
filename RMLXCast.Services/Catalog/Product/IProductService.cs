using RMLXCast.Core.Domain.Catalog;

namespace RMLXCast.Services.Catalog
{
    public interface IProductService
    {
        Task CreateProductAsync(Product product);
        Task<Product?> GetProductByIdAsync(int productId);
        Task UpdateProductAsync(Product product);
        Task<bool> DeleteProductByIdAsync(int productId);
    }
}