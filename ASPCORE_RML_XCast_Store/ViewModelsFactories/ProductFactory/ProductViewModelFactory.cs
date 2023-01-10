using RMLXCast.Core.Domain.Catalog;
using RMLXCast.Web.ViewModels;

namespace RMLXCast.Web.ViewModelsFactories.ProductFactory
{
    public class ProductViewModelFactory : IProductViewModelFactory
    {
        public ProductViewModel GetProductViewModel(Product product)
        {
            return new ProductViewModel()
            {

            };
        }
    }
}
