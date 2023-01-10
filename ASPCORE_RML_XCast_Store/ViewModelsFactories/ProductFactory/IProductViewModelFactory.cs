using RMLXCast.Core.Domain.Catalog;
using RMLXCast.Web.ViewModels;

namespace RMLXCast.Web.ViewModelsFactories.ProductFactory
{
    public interface IProductViewModelFactory
    {
        ProductViewModel GetProductViewModel(Product product);
    }
}