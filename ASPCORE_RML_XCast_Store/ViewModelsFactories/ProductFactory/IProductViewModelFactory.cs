using RMLXCast.Core.Domain.Catalog;
using RMLXCast.Web.ViewModels.Product;

namespace RMLXCast.Web.ViewModelsFactories.ProductFactory
{
    public interface IProductViewModelFactory
    {
        ProductViewModel GetProductViewModel(Product product);
    }
}