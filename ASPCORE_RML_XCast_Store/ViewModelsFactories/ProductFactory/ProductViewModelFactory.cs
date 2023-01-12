using RMLXCast.Core.Domain.Catalog;
using RMLXCast.Web.ViewModels.Product;

namespace RMLXCast.Web.ViewModelsFactories.ProductFactory
{
    public class ProductViewModelFactory : IProductViewModelFactory
    {
        public ProductViewModel GetProductViewModel(Product product)
        {
            return new ProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                ShortDescription = product.ShortDescription,
                FullDescription = product.FullDescription,
                AdminComment = product.AdminComment,
                AllowCustomerReviews = product.AllowCustomerReviews,
                OrderMaximumQuantity = product.OrderMaximumQuantity,
                OrderMinimumQuantity = product.OrderMinimumQuantity,
                Price = product.Price,
                Published = product.Published,
            };
        }
    }
}
