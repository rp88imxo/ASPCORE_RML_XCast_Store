using RMLXCast.Core.Domain.Catalog;

namespace RMLXCast.Services.Catalog.Stocks
{
    public interface IProductStockService
    {
        Task<IList<Stock>?> GetAllStockForProduct(int productId);
    }
}